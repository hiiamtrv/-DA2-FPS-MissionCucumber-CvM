using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractState
{
    public class Obtaining : BaseState
    {
        const float OBTAIN_TIME = 3;

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
            if (!PlayerMoved() && InputMgr.DoInteract())
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
            if (this._obtainTime >= OBTAIN_TIME)
            {
                Debug.Log("Obtaining DONE !");
                this._stateMachine.Destroy();
            }
        }
    }
}