using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HawkDrone
{
    public class Fly : BaseState
    {
        float _x;
        public float X => _x;

        float _z;
        public float Z => _z;
        
        public Fly(StateMachine stateMachine) : base(stateMachine) { }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
            this._x = InputMgr.XMove(this._gameObject);
            this._z = InputMgr.ZMove(this._gameObject);
        }
    }
}
