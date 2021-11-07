using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    namespace MoveState
    {
        public class Jump : Base
        {
            public Jump(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnEnter()
            {
                this.Jump();
            }

            public override void PhysicUpdate()
            {
                this.Walk(WalkMode.X_ONLY);
                base.PhysicUpdate();
            }

            protected override void CheckNextState()
            {
                bool isGrounded = this._charCtrl.isGrounded;
                if (isGrounded)
                {
                    Stand standState = new Stand(this._stateMachine);
                    this.SetNextState(standState);
                }
            }
        }
    }
}