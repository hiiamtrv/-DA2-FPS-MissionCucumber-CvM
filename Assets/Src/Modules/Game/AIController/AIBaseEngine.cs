using System.Collections;
using System.Collections.Generic;
using Character;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Weapons;

namespace AI
{
    public class AIBaseEngine : StateMachine, IAITrigger
    {
        public NavMeshAgent agent { get; private set; }

        protected CharacterSide side;
        public CharacterSide Side => side;

        [SerializeField] protected float _nerfScale;

        protected Camera eye;
        public Camera Eye => eye;

        [SerializeField] protected GameObject _gun;
        protected IWeapon _gunEngine;

        [SerializeField] public float NoticeRange { get; set; }
        [SerializeField] public float AttackRange { get; set; }

        protected PhotonView view;

        void Awake()
        {
            agent = this.GetComponent<NavMeshAgent>();
            view = this.GetComponent<PhotonView>();
            side = this.GetComponent<CharacterStats>().CharacterSide;
            eye = this.GetComponent<Eye>().MainCamera;
        }

        protected override void Update()
        {
            base.Update();
            agent.speed = this.GetComponent<CharacterStats>().Speed * this._nerfScale;
        }

        protected override void LateUpdate()
        {
            //TODO: Check for next state change
            base.LateUpdate();
        }

        public override BaseState GetDefaultState() => new CheckCucumberPoint(this);

        public virtual BaseState RollNextState()
        {
            return this.GetDefaultState();
        }

        public virtual void OnEndAction()
        {
            BaseState nextState = this.RollNextState();
            this.ChangeState(nextState);
        }

        public virtual void OnSpotEnemy()
        {

        }

        public virtual void OnLostTarget()
        {

        }

        public virtual void OnTargetDead()
        {

        }

        public virtual void OnMeetInteractable()
        {

        }

        public virtual void OnDamaged()
        {

        }

        public virtual void OnShieldOut()
        {
            //TODO: CAT OVERRIDE
        }
    }
}
