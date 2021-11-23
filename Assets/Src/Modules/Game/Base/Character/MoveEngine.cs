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
            
            this.SubEventCat();
        }

        protected override void Start()
        {
            this.Model = this.GetComponent<CharacterStats>().MoveModel;
            base.Start();
        }
        
        public override BaseState GetDefaultState() => new MoveState.Stand(this);

        void SubEventCat()
        {
            CharacterStats stast = this.GetComponent<CharacterStats>();
            if (stast.CharacterSide == CharacterSide.CATS)
            {
                EventCenter.Subcribe(EventId.CAT_DYING, (object pubData) =>
                {
                    GameObject dispatcher = pubData as GameObject;
                    if (dispatcher == this.gameObject)
                    {
                        this.Model.JumpHeightPercent *= 1.5f;
                        this.Model.SpeedPercent *= 2;
                    }
                });
            }
        }
    }
}

