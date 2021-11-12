using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MoveEngine : StateMachine
    {
        MoveModel _model;
        public MoveModel Model { get => this._model; private set => this._model = value; }

        protected override void Start()
        {
            this.Model = new MoveModel();
            base.Start();
            this.ResetStats();
        }

        protected void ResetStats()
        {

        }

        public override BaseState GetDefaultState() => new MoveState.Stand(this);
    }
}

