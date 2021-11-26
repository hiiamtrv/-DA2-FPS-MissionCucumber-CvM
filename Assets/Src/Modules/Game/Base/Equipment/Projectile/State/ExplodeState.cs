using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    namespace State
    {
        public class ExplodeState : BaseState, IProjectileState
        {
            public ProjectileEngine StateMachine => (this._stateMachine as ProjectileEngine);

            public ExplodeState(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnEnter()
            {
                this.StateMachine.HitObject();
            }

            public void OnCollsion()
            {
                //do nothing cuz its exploded immediately
            }
        }
    }
}