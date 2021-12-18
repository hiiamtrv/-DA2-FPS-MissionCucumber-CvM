using System.Collections;
using System.Collections.Generic;
using Character.HealthState;
using UnityEngine;

namespace Character
{
    public class HealthEngine : StateMachine
    {
        [SerializeField] protected AudioClip _hitSound;

        HealthModel _model;
        public HealthModel Model { get => this._model; private set => this._model = value; }

        protected HealthState.Base CurrentState => this._currentState as HealthState.Base;
        public override BaseState GetDefaultState() => new HealthState.Base(this);


        protected override void Start()
        {
            this.Model = this.GetComponent<CharacterStats>().HealthModel;
            base.Start();

            EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, (e) =>
            {
                GameObject dieObject = e as GameObject;
                if (dieObject == this.gameObject && this.gameObject.activeInHierarchy)
                {
                    ((Base)this._currentState).ForceDie();
                }
            });
        }

        public void GainShield(float amount, ShieldReason reason = ShieldReason.DEFAULT, GameObject sender = null)
        {
            this.CurrentState.OnShieldChange(amount, reason, sender);
        }

        public void InflictDamage(float damage, DamageReason reason = DamageReason.DEFAULT, float penetration = 0, GameObject sender = null)
        {
            this.gameObject.PlaySound(_hitSound);
            float shieldDamage = Mathf.Min(damage * (1 - penetration), this.Model.Shield);
            float trueDamage = damage - shieldDamage;
            if (this.Model.Shield > 0)
            {
                float remainShield = this.Model.Shield - shieldDamage;
                this.CurrentState.OnShieldChange(remainShield, ShieldReason.DAMAGE, sender);
            }
            this.CurrentState.OnDamaged(trueDamage, reason, sender);
        }

        public void Heal(float amount, HealReason reason, bool exceedMaxHealth = false, GameObject sender = null)
        {
            this.CurrentState.OnHealed(amount, reason, exceedMaxHealth, sender);
        }
    }
}

