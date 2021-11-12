using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    namespace State
    {
        public class Interacting : BaseState
        {
            GameObject _obtainPlayer;
            Vector3 _obtainPos;
            float _interactTime;

            public Interacting(StateMachine stateMachine, GameObject obtainPlayer) : base(stateMachine)
            {
                this._obtainPlayer = obtainPlayer;
            }

            public override void OnEnter()
            {
                Debug.Log("Start obtaining");
                this._obtainPos = this._obtainPlayer.transform.position;
                this._interactTime = 0;
            }

            public override void LogicUpdate()
            {
                if (!PlayerMoved() && InputMgr.Interact)
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

            bool PlayerMoved()
            {
                Vector3 curPos = this._obtainPlayer.transform.position;
                Vector3 obtainPos = this._obtainPos;
                return (curPos != obtainPos ? true : false);
            }

            void CheckObtainDone()
            {
                float INTERACT_TIME = ((InteractEngine) this._stateMachine).Model.InteractTime;
                if (this._interactTime >= INTERACT_TIME)
                {
                    Debug.Log("Obtaining DONE !");

                    DoneInteract stateObtained = new DoneInteract(this._stateMachine);
                    this.SetNextState(stateObtained);
                }
            }
        }
    }
}