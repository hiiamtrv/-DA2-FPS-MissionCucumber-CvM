using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletState
{
    public class Base : BaseState
    {
        protected float Speed
        {
            get => ((BulletEngine)this._stateMachine).GetSpeed();
            set => ((BulletEngine)this._stateMachine).SetSpeed(value);
        }

        public Base(StateMachine stateMachine) : base(stateMachine) { }
    }
}