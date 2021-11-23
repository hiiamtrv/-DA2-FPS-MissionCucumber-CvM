using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class AimEngine : StateMachine
    {
        Camera _eye;
        public Camera Eye => this._eye;

        protected override void Start()
        {
            this._eye = this.GetComponentInChildren<Camera>();
            base.Start();
        }

        public override BaseState GetDefaultState() => new AimState.Enabled(this);
    }
}

