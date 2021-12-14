using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace HawkDrone
{
    public class HawkDroneHealth : HealthEngine
    {
        public override BaseState GetDefaultState() => new HawkHealthState(this);
    }
}