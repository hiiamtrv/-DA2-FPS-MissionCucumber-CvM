using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    namespace MoveState
    {
        public class Stand : Base
        {
            public Stand(StateMachine stateMachine) : base(stateMachine) { }

            public override void PhysicUpdate()
            {
                this.MoveX = 0;
                this.MoveZ = 0;
                this.Walk();
                base.PhysicUpdate();
            }

            protected override void CheckNextState()
            {
                bool jump = InputMgr.Jump(this._gameObject);
                if (jump)
                {
                    Jump jumpState = new Jump(this._stateMachine);
                    this.SetNextState(jumpState);
                }
            }
        }
    }
}
