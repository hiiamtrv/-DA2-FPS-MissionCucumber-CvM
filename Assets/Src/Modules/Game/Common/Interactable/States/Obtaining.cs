using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    namespace State
    {
        public class Obtaining : BaseState
        {
            GameObject _obtainPlayer;
            Vector3 _obtainPos;
            float _obtainTime;

            public Obtaining(StateMachine stateMachine, GameObject obtainPlayer) : base(stateMachine)
            {
                this._obtainPlayer = obtainPlayer;
            }

            public override void OnEnter()
            {
                Debug.Log("Start obtaining");
                this._obtainPos = this._obtainPlayer.transform.position;
                this._obtainTime = 0;
            }

            public override void LogicUpdate()
            {
                if (!PlayerMoved() && InputMgr.Interact)
                {
                    this._obtainTime += Time.deltaTime;
                    Debug.Log("Obtaining: " + this._obtainTime);
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
                if (this._obtainTime >= Stats.OBTAIN_TIME)
                {
                    Debug.Log("Obtaining DONE !");

                    Obtained stateObtained = new Obtained(this._stateMachine);
                    this.SetNextState(stateObtained);
                }
            }
        }
    }
}