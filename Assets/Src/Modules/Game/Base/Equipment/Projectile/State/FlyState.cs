using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    namespace State
    {
        public class FlyState : BaseState, IProjectileState
        {
            public ProjectileEngine StateMachine => (this._stateMachine as ProjectileEngine);
            public float FlySpeed => StateMachine.FlySpeed;

            public FlyState(StateMachine stateMachine) : base(stateMachine) { }
            protected BaseState ExplodeState => new ExplodeState(this._stateMachine);

            public override void PhysicUpdate()
            {
                Vector3 vectorMove = this.StateMachine.FlyVector * Time.deltaTime * this.FlySpeed;
                this._gameObject.transform.Translate(vectorMove, Space.Self);
                base.PhysicUpdate();
            }

            public void OnCollsion()
            {
                //TODO: Apply bounce
                this.SetNextState(ExplodeState);
            }
        }
    }
}