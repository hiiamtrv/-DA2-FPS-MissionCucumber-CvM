using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace AI
{
    public class CheckCucumberPoint : BaseState, IAIState
    {
        public bool IsTargetLockMode() { return false; }

        public AIBaseEngine StateMachine => (AIBaseEngine)this._stateMachine;
        public CheckCucumberPoint(StateMachine stateMachine) : base(stateMachine) { }

        public override void OnEnter()
        {
            base.OnEnter();
            this.StateMachine.GetComponent<Eye>().ResetRotation();
            this.StateMachine.GotoRandomCucumberPos();
        }

        protected override void CheckNextState()
        {
            Vector3 destination = this.StateMachine.agent.destination;
            if (Vector3.Distance(destination, this._gameObject.transform.position) <= AIUtils.MIN_ACCEPTABLE_DISTANCE)
            {
                this.StateMachine.OnEndAction();
            }
        }

        public override void OnExit()
        {
            this.StateMachine.agent.destination = this._gameObject.transform.position;
            base.OnExit();
        }
    }
}
