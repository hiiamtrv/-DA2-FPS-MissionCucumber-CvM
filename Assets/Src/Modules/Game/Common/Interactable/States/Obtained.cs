using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    namespace State
    {
        public class Obtained : BaseState
        {
            public Obtained(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnEnter()
            {
                this._stateMachine.Destroy();
            }
        }
    }
}
