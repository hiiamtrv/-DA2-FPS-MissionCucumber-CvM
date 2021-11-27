using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    namespace State
    {
        public class Interacting : BaseState
        {
            protected float _interactTime;
            public float InteractTime => _interactTime;

            protected bool _isSuccessful;

            protected InteractEngine StateMachine => ((InteractEngine)this._stateMachine);
            protected InteractModel Model => StateMachine.Model;
            public GameObject InteractPlayer => StateMachine.InteractPlayer;

            //ADJACENTS STATES
            protected virtual BaseState NextStateIdle => new Idle(this._stateMachine);
            protected virtual BaseState NextStateDoneInteract => new DoneInteract(this._stateMachine);

            public Interacting(StateMachine stateMachine) : base(stateMachine)
            {
                this._interactTime = 0;
                this._isSuccessful = false;
            }

            public override void OnEnter()
            {

                EventCenter.Publish(
                    EventId.INTERACT_START,
                    new PubData.IneractStart(this.InteractPlayer, this._interactTime, this.Model)
                );

                if (!this.Model.CanMoveWhileInteract)
                {
                    Character.MoveEngine charMoveEngine = this.InteractPlayer.GetComponent<Character.MoveEngine>();
                    if (charMoveEngine) charMoveEngine.ChangeState(new Character.MoveState.Immobilized(charMoveEngine));
                }
            }

            public override void OnExit()
            {
                if (!this.Model.CanMoveWhileInteract)
                {
                    Character.MoveEngine charMoveEngine = this.InteractPlayer.GetComponent<Character.MoveEngine>();
                    if (charMoveEngine) charMoveEngine.ChangeState(new Character.MoveState.Stand(charMoveEngine));
                }

                if (!this._isSuccessful) this.StateMachine.OnInteractFailed();
                base.OnExit();
            }

            public override void LogicUpdate()
            {
                if (!this.IsPlayerInRange || InputMgr.EndInteract)
                {
                    Idle stateIdle = this.NextStateIdle as Idle;
                    this.SetNextState(stateIdle);

                    EventCenter.Publish(
                        EventId.INTERACT_END,
                        new PubData.InteractEnd(this.InteractPlayer, this._gameObject, false)
                    );
                }
                else
                {
                    this._interactTime += Time.deltaTime;
                    this.CheckObtainDone();
                }
            }

            void CheckObtainDone()
            {
                float INTERACT_TIME = this.Model.InteractTime;
                if (this._interactTime >= INTERACT_TIME)
                {
                    this._isSuccessful = true;
                    BaseState stateObtained = this.NextStateDoneInteract;
                    this.SetNextState(stateObtained);
                }
            }

            bool IsPlayerInRange => ((InteractEngine)this._stateMachine).IsPlayerInRange(this.InteractPlayer);
        }
    }
}