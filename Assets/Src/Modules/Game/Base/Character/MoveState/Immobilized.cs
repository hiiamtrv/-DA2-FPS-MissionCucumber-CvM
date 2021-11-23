using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    namespace MoveState
    {
        public class Immobilized : Base
        {
            public Immobilized(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnEnter()
            {
                // EventCenter.Subcribe(EventId.CHARACTER_DONE_INTERACT, (args) => this.StopStanding());
            }

            public override void PhysicUpdate()
            {
                this.MoveX = 0;
                this.MoveZ = 0;
                base.PhysicUpdate();
            }

            void StopStanding()
            {
                Stand stateStand = new Stand(this._stateMachine);
                this.SetNextState(stateStand);
            }
        }
    }
}
