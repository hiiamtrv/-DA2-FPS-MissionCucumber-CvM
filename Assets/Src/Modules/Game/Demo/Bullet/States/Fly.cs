using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletState
{
    public class Fly : Base
    {
        float TIME_EXPLOSION = 3f;

        public Fly(StateMachine stateMachine) : base(stateMachine) { }

        public override void OnEnter()
        {
            LeanTween.delayedCall(TIME_EXPLOSION, () => this.Explode());
        }

        public override void PhysicUpdate()
        {
            Vector3 vtBase = this.Speed * Vector3.forward;
            this._stateMachine.transform.Translate(vtBase, Space.Self);
        }

        void Explode()
        {
            Explode stateExplode = new Explode(this._stateMachine);
            this.SetNextState(stateExplode);
        }
    }
}