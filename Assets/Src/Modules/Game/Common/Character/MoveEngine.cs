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
            EventCenter.Subcribe(
                EventId.MATCH_END,
                (object data) => this.ChangeState(new MoveState.Immobilized(this))
            );
        }

        protected override void Start()
        {
            this.Model = this.GetComponent<CharacterStats>().MoveModel;
            base.Start();
            this.ResetStats();
        }

        protected void ResetStats()
        {

        }

        public override BaseState GetDefaultState() => new MoveState.Stand(this);
    }
}

