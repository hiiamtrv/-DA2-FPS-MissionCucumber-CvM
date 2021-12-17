using System.Collections;
using System.Collections.Generic;
using Character;
using Character.HealthState;
using UnityEngine;

namespace Cats
{
    namespace HealthState
    {
        public class CatNormal : Base
        {
            public CatNormal(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnDamaged(float damage, DamageReason reason, GameObject sender)
            {
                // if (this.Model.Shield <= 0) base.OnDamaged(damage, reason, sender);
            }

            protected override void OnShieldOut()
            {
                CatDown stateCatDown = new CatDown(this._stateMachine);
                this.SetNextState(stateCatDown);
            }
        }
    }
}