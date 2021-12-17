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
                this._interactTime = this.GetInteractTime();
                this._isSuccessful = false;
            }

            public override void OnEnter()
            {
                PubData.IneractStart pubData = new PubData.IneractStart(this.InteractPlayer, this._interactTime, this._gameObject, this.Model.InteractTime);
                EventCenter.Publish(EventId.INTERACT_START, pubData);
                NetworkGame.Publish(EventId.INTERACT_START, pubData.Serialize());

                if (!this.Model.CanMoveWhileInteract)
                {
                    Character.MoveEngine charMoveEngine = this.InteractPlayer.GetComponent<Character.MoveEngine>();
                    if (charMoveEngine != null) charMoveEngine.ChangeState(new Character.MoveState.Immobilized(charMoveEngine));
                }

                EventCenter.Subcribe(EventId.INTERACT_END, this.OnReceiveEndSyncEvent);
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
                Debug.Log("Logic update state interacting");
                Debug.Log("Condition",
                    !this.IsPlayerInRange,
                    !this.StateMachine.IsPlayerAi,
                    InputMgr.EndInteract(this.InteractPlayer),
                    !this.StateMachine.IsPlayerLooking(this.InteractPlayer)
                );

                if (!this.IsPlayerInRange
                    || (!this.StateMachine.IsPlayerAi && InputMgr.EndInteract(this.InteractPlayer))
                    || (!this.StateMachine.IsPlayerAi && !this.StateMachine.IsPlayerLooking(this.InteractPlayer))
                )
                {
                    BaseState stateIdle = this.NextStateIdle;
                    this.SetNextState(stateIdle);

                    PubData.InteractEnd pubData = new PubData.InteractEnd(this.InteractPlayer, this._gameObject, false);
                    EventCenter.Publish(EventId.INTERACT_END, pubData);
                    NetworkGame.Publish(EventId.INTERACT_END, pubData.Serialize());
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

            protected virtual float GetInteractTime()
            {
                return 0;
            }

            protected void OnReceiveEndSyncEvent(object pubData)
            {
                PubData.InteractEnd data = pubData as PubData.InteractEnd;
                if (data.InteractObject == this._gameObject)
                {
                    if (data.IsSuccessful)
                    {
                        this._interactTime = float.MaxValue;
                    }
                    else
                    {
                        BaseState stateIdle = this.NextStateIdle;
                        this.SetNextState(stateIdle);
                    }
                }
            }
        }
    }
}