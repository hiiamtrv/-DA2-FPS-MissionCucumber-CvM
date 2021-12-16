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

        [SerializeField] protected float _powerScale;

        protected Camera eye;
        public Camera Eye => eye;

        [SerializeField] protected GameObject _gun;
        public IWeapon Weapon { get; private set; }

        [SerializeField] public float NoticeRange;
        [SerializeField] public float AttackRange;

        protected PhotonView view;

        public IInteractable InteractingObject;

        void Awake()
        {
            agent = this.GetComponent<NavMeshAgent>();
            view = this.GetComponent<PhotonView>();
            side = this.GetComponent<CharacterStats>().CharacterSide;
            eye = this.GetComponent<Eye>().MainCamera;

            if (this._gun.GetComponent<MeleeWeapon>() != null) this.Weapon = this._gun.GetComponent<MeleeWeapon>();
            else if (this._gun.GetComponent<AmmoWeapon>() != null) this.Weapon = this._gun.GetComponent<AmmoWeapon>();

            EventCenter.Subcribe(EventId.MATCH_END, (data) => this.enabled = false);

            if (!PhotonNetwork.IsMasterClient)
            {
                this.enabled = false;
            }
        }

        protected override void Update()
        {
            base.Update();
            agent.speed = this.GetComponent<CharacterStats>().Speed * this._powerScale;
        }

        protected override void LateUpdate()
        {
            //TODO: Check for next state change

            if (this.IsInTargetLockState())
            {

            }
            else
            {
                //Check for spotted enemy
                List<GameObject> spottedEnemies = this.GetSpottedEnemies();
                if (spottedEnemies.Count > 0) this.OnSpotEnemy(spottedEnemies);

                //Check for spotted interactable
                List<IInteractable> interactables = this.GetNearInteractables();
                if (interactables.Count > 0) this.OnMeetInteractable(interactables);
            }

            base.LateUpdate();
        }

        public override BaseState GetDefaultState() => new Patrol(this);

        public virtual BaseState RollNextState()
        {
            return this.GetDefaultState();
        }

        public virtual void OnEndAction()
        {
            BaseState nextState = this.RollNextState();
            this.ChangeState(nextState);
        }

        public virtual void OnSpotEnemy(List<GameObject> enemies)
        {
            //TODO: override
        }

        public virtual void OnLostTarget(GameObject target)
        {
            this.OnEndAction();
        }

        public virtual void OnTargetDead(GameObject target)
        {
            this.OnEndAction();
        }

        public virtual void OnMeetInteractable(List<IInteractable> interactObject)
        {
            //TODO: with cat, no interract
            //TODO: with mouse, interract if cucumber
        }

        public virtual void OnDamaged(GameObject attacker)
        {

        }

        public virtual void OnShieldOut()
        {
            //TODO: CAT OVERRIDE
        }

        bool IsInTargetLockState()
        {
            return (this._currentState as IAIState).IsTargetLockMode();
        }

        public void AbortInteract()
        {
            this.InteractingObject.AbortInteract(this.gameObject);
        }
    }
}
