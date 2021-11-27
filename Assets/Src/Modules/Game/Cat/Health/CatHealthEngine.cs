using System.Collections;
using System.Collections.Generic;
using Cats.HealthState;
using Character;
using UnityEngine;

namespace Cats
{
    public class CatHealthEngine : HealthEngine
    {
        public override BaseState GetDefaultState() => new CatNormal(this);

        protected virtual BaseState CatWeakened => new CatWeakened(this);

        protected override void Start()
        {
            EventCenter.Subcribe(
                EventId.SHIELD_CENTER_DESTROYED,
                (object pubData) =>
                {
                    Debug.Log("Change Cat State to weakened !");
                    this.ChangeState(this.CatWeakened);
                }
            );
            base.Start();
        }
    }
}
