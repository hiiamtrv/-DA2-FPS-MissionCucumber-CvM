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
            // ObjectPool.Retrieve(this._gameObject);
            this.DestroyGameObject();
        }
    }
}
