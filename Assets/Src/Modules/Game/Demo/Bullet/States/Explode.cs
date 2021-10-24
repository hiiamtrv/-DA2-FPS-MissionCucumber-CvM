using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletState
{
    public class Explode : Base
    {
        public Explode(StateMachine stateMachine) : base(stateMachine) { }

        public override void OnEnter()
        {
            this.DestroyGameObject();
        }
    }
}
