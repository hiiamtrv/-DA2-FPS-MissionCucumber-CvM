using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    namespace State
    {
        public class Idle : BaseState
        {
            protected InteractEngine StateMachine => ((InteractEngine)this._stateMachine);

            public Idle(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnEnter()
            {
                this.StateMachine.ResetInteractPlayer();
                base.OnEnter();
            }
        }
    }
}
