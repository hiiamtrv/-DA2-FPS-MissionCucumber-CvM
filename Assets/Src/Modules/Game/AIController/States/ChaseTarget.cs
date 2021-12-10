using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using Weapons;

namespace AI
{
    public class ChaseTarget : BaseState, IAIState
    {
        const float DISTANCE = 20;

        public bool IsTargetLockMode() { return true; }
        public GameObject Target { get; private set; }

        bool waitingForNextShoot = false;

        public AIBaseEngine StateMachine => (AIBaseEngine)this._stateMachine;
        public ChaseTarget(StateMachine stateMachine) : base(stateMachine) { }


        public void SetTarget(GameObject target)
        {
            this.Target = target;
        }

        public void SetTarget(List<GameObject> targets)
        {
            this.Target = Utils.PickFromList(targets);
        }

        public override void OnEnter()
        {
            this.waitingForNextShoot = false;
            base.OnEnter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!this.StateMachine.CanSeeTarget(this.Target))
            {
                if (this.Target.activeInHierarchy) this.StateMachine.OnLostTarget(this.Target);
                else this.StateMachine.OnTargetDead(this.Target);
            }
            else
            {
                if (!this.waitingForNextShoot && this.StateMachine.IsEnemyAttackable(this.Target))
                {
                    IWeapon weapon = this.StateMachine.Weapon;
                    weapon.TriggerAttack();

                    float waitTime = (weapon is AmmoWeapon
                        ? 1 / (weapon as AmmoWeapon).Model.FireRate
                        : 1 / (weapon as MeleeWeapon).Model.AttackSpeed
                    );
                    this.waitingForNextShoot = true;
                    LeanTween.delayedCall(Mathf.Max(Time.deltaTime, waitTime), () => this.waitingForNextShoot = false);

                    float remainAmmo = (weapon is AmmoWeapon ? (weapon as AmmoWeapon).Model.RemainAmmo : 0);
                    Debug.Log("Attack", weapon, remainAmmo, waitTime);
                }
            }
        }

        public override void LateUpdate()
        {
            this.StateMachine.GetComponent<Eye>().LookAt(this.Target);

            float distance = Vector3.Distance(this._gameObject.transform.position, this.Target.transform.position);
            if (distance <= DISTANCE)
            {
                this.StateMachine.agent.SetDestination(this._gameObject.transform.position);
            }
            else
            {
                this.StateMachine.agent.SetDestination(this.Target.transform.position);
            }
        }

        protected override void CheckNextState()
        {

        }

        public override void OnExit()
        {
            this.StateMachine.agent.destination = this._gameObject.transform.position;
            base.OnExit();
        }
    }
}
