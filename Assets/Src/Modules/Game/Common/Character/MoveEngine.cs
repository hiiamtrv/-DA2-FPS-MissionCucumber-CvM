using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MoveEngine : StateMachine
    {
        MoveModel _model;
        public MoveModel Model { get => this._model; private set => this._model = value; }

        void Awake()
        {
            this.Model = new MoveModel();
        }

        protected override void Start()
        {
            base.Start();
            this.ResetStats();
        }

        public void ResetStats()
        {

        }

        public override BaseState GetDefaultState() => new MoveState.Stand(this);
    }
}

