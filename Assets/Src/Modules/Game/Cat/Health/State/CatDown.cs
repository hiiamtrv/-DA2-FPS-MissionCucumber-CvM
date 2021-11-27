using System.Collections;
using System.Collections.Generic;
using Character;
using Character.HealthState;
using UnityEngine;

namespace Cats
{
    namespace HealthState
    {
        public class CatDown : Base
        {
            public CatDown(StateMachine stateMachine) : base(stateMachine) { }
            MoveModel _playerMoveModel;

            float _slow;
            float _downTime;
            float _receivedShield;

            public override void OnEnter()
            {
                CatDownModel model = this._gameObject.GetComponent<CatDownStats>().Model;
                this._slow = model.Slow;
                this._downTime = model.TimeDown;
                this._receivedShield = model.ShieldReceived;

                MoveEngine moveEngine = this._gameObject.GetComponent<MoveEngine>();
                if (moveEngine != null)
                {
                    this._playerMoveModel = this._gameObject.GetComponent<MoveEngine>().Model;
                    this._playerMoveModel.SpeedPercent -= this._slow;
                    this._playerMoveModel.JumpHeightPercent -= this._slow;
                }
                else this._playerMoveModel = null;

                LeanTween.delayedCall(this._downTime, () =>
                {
                    this.SetNextState(new CatNormal(this._stateMachine));
                });

                EventCenter.Publish(
                    EventId.CAT_DOWN,
                    new PubData.CatDown(this._stateMachine.gameObject, this._downTime)
                );
                base.OnEnter();
            }

            public override void OnExit()
            {
                this.OnShieldChange(this._receivedShield, ShieldReason.CAT_RECOVERED, this._gameObject);
                if (this._playerMoveModel != null)
                {
                    this._playerMoveModel.SpeedPercent += this._slow;
                    this._playerMoveModel.JumpHeightPercent += this._slow;
                }
                EventCenter.Publish(EventId.CAT_RECOVERED, this._stateMachine.gameObject);
                base.OnExit();
            }

            public override void OnDamaged(float damage, DamageReason reason, GameObject sender)
            {
                //do nothing
            }

            public override void OnShieldChange(float amount, ShieldReason reason, GameObject sender)
            {
                if (reason == ShieldReason.CAT_RECOVERED) base.OnShieldChange(amount, reason, sender);
            }
        }
    }
}