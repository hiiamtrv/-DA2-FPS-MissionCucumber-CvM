using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.HealthState;

namespace HawkDrone
{
    public class HawkHealthState : Base
    {
        public HawkHealthState(StateMachine stateMachine) : base(stateMachine) { }

        protected override void OnHealthOut()
        {
            //no inherit
            //do nothing
        }
    }
}
