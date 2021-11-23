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
            protected InteractModel Model => ((InteractEngine)this._stateMachine).Model;
            public GameObject InteractPlayer => ((InteractEngine) this._stateMachine).InteractPlayer;

            //ADJACENTS STATES
            protected BaseState NextStateIdle => new Idle(this._stateMachine);
            protected BaseState NextStateDoneInteract => new DoneInteract(this._stateMachine);

            public Interacting(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnEnter()
            {
                Debug.Log("Start obtaining");

                this._interactTime = 0;
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
                    Debug.Log("Player is still in range");
                    this._interactTime += Time.deltaTime;
                    this.CheckObtainDone();
                }
            }

            void CheckObtainDone()
            {
                float INTERACT_TIME = this.Model.InteractTime;
                if (this._interactTime >= INTERACT_TIME)
                {
                    DoneInteract stateObtained = this.NextStateDoneInteract as DoneInteract;
                    this.SetNextState(stateObtained);
                }
            }

            bool IsPlayerInRange => ((InteractEngine)this._stateMachine).IsPlayerInRange(this.InteractPlayer);
        }
    }
}