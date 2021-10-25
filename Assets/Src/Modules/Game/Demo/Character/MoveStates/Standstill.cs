using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterMoveState
{
    public class Standstill : Base
    {
        public Standstill(StateMachine stateMachine) : base (stateMachine){}

        public override void OnEnter()
        {
            EventCenter.Subcribe(EventId.CHARACTER_DONE_INTERACT, (args) => this.StopStanding());
        }

        public override void PhysicUpdate()
        {
            this.MoveX = 0;
            this.MoveZ = 0;
            base.PhysicUpdate();
        }

        void StopStanding() {
            Run stateRun = new Run(this._stateMachine);
            this.SetNextState(stateRun);
        }
    }
}
