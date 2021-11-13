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
            Model Model => ((InteractEngine)this._stateMachine).Model;

            public Interacting(StateMachine stateMachine, GameObject obtainPlayer) : base(stateMachine)
            {
                this._interactPlayer = obtainPlayer;
            }

            public override void OnEnter()
            {
                Debug.Log("Start obtaining");
                this._interactTime = 0;

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
                if (this.IsPlayerInRange && InputMgr.Interact)
                {
                    this._interactTime += Time.deltaTime;
                    Debug.Log("Obtaining: " + this._interactTime);
                    this.CheckObtainDone();
                }
                else
                {
                    Idle stateIdle = new Idle(this._stateMachine);
                    this.SetNextState(stateIdle);
                }
            }

            void CheckObtainDone()
            {
                float INTERACT_TIME = this.Model.InteractTime;
                if (this._interactTime >= INTERACT_TIME)
                {
                    Debug.Log("Obtaining DONE !");

                    DoneInteract stateObtained = new DoneInteract(this._stateMachine);
                    this.SetNextState(stateObtained);
                }
            }

            bool IsPlayerInRange => ((InteractEngine)this._stateMachine).IsPlayerInRange(this._interactPlayer);
        }
    }
}