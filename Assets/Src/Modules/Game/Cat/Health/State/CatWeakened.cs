using System.Collections;
using System.Collections.Generic;
using Character;
using Character.HealthState;
using UnityEngine;

namespace Cats
{
    namespace HealthState
    {
        public class CatWeakened : Base
        {
            public CatWeakened(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnEnter()
            {
                base.OnShieldChange(0, ShieldReason.CAT_WEAKENED, this._gameObject);
                base.OnShieldOut();
                base.OnEnter();
            }

            public override void OnShieldChange(float amount, ShieldReason reason, GameObject sender)
            {
                //do nothing
            }

            protected override void OnShieldOut()
            {
                //do nothing
            }
        }
    }
}
