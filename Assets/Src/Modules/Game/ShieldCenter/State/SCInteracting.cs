using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable.State;

namespace ShieldCenter
{
    public class SCInteracting : Interacting
    {
        public SCInteracting(StateMachine stateMachine, float startTime) : base(stateMachine)
        {
            this._interactTime = startTime;
        }
    }
}