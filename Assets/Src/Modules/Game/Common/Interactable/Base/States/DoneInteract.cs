using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    namespace State
    {
        public class DoneInteract : BaseState
        {
            public DoneInteract(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnEnter()
            {
                // this._stateMachine.Destroy();
                this._gameObject.SetActive(false);
            }
        }
    }
}
