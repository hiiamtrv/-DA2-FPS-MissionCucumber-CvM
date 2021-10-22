using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterMoveState
{
    public class Run : Base
    {
        public Run(StateMachine stateMachine) : base(stateMachine) { }

        public override void PhysicUpdate()
        {
            this.MoveX = 0;
            this.MoveZ = 0;
            this.Walk();
            base.PhysicUpdate();
        }

        protected override void CheckNextState()
        {
            bool jump = InputMgr.DoJump();
            if (jump)
            {
                Jump jumpState = new Jump(this._stateMachine);
                this.SetNextState(jumpState);
            }
        }
    }
}
