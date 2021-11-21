using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    namespace State
    {
        public class Interacting : BaseState
        {
            GameObject _interactPlayer;
            float _interactTime;
            InteractModel Model => ((InteractEngine)this._stateMachine).Model;

            public Interacting(StateMachine stateMachine, GameObject obtainPlayer) : base(stateMachine)
            {
                this._interactPlayer = obtainPlayer;
            }

            public override void OnEnter()
            {
                Debug.Log("Start obtaining");

                this._interactTime = 0;
                EventCenter.Publish(
                    EventId.INTERACT_START,
                    new PubData.IneractStart(this._interactPlayer, this._interactTime, this.Model)
                );

                if (!this.Model.CanMoveWhileInteract)
                {
                    Character.MoveEngine charMoveEngine = this._interactPlayer.GetComponent<Character.MoveEngine>();
                    if (charMoveEngine) charMoveEngine.ChangeState(new Character.MoveState.Immobilized(charMoveEngine));
                }
            }

            public override void OnExit()
            {
                if (!this.Model.CanMoveWhileInteract)
                {
                    Character.MoveEngine charMoveEngine = this._interactPlayer.GetComponent<Character.MoveEngine>();
                    if (charMoveEngine) charMoveEngine.ChangeState(new Character.MoveState.Stand(charMoveEngine));
                }
                base.OnExit();
            }

            public override void LogicUpdate()
            {
                if (!this.IsPlayerInRange || InputMgr.EndInteract)
                {
                    Idle stateIdle = new Idle(this._stateMachine);
                    this.SetNextState(stateIdle);

                    EventCenter.Publish(
                        EventId.INTERACT_END,
                        new PubData.InteractEnd(this._interactPlayer, this._gameObject, false)
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
                    DoneInteract stateObtained = new DoneInteract(this._stateMachine);
                    this.SetNextState(stateObtained);

                    EventCenter.Publish(
                        EventId.INTERACT_END,
                        new PubData.InteractEnd(this._interactPlayer, this._gameObject, true)
                    );
                }
            }

            bool IsPlayerInRange => ((InteractEngine)this._stateMachine).IsPlayerInRange(this._interactPlayer);
        }
    }
}