using System.Collections;
using System.Collections.Generic;
using Character.HealthState;
using UnityEngine;

namespace Character
{
    public class HealthEngine : StateMachine
    {
        HealthModel _model;
        public HealthModel Model { get => this._model; private set => this._model = value; }

        protected HealthState.Base CurrentState => this._currentState as HealthState.Base;
        public override BaseState GetDefaultState() => new HealthState.Base(this);


        protected override void Start()
        {
            this.Model = this.GetComponent<CharacterStats>().HealthModel;
            base.Start();
        }

        public void GainShield(float amount, ShieldReason reason = ShieldReason.DEFAULT)
        {
            this.CurrentState.OnShieldChange(amount, reason);
        }

        public void InflictDamage(float damage, DamageReason reason = DamageReason.DEFAULT, float penetration = 0)
        {
            float shieldDamage = Mathf.Min(damage * (1 - penetration), this.Model.Shield);
            float trueDamage = damage - shieldDamage;
            if (this.Model.Shield > 0)
            {
                float remainShield = this.Model.Shield - shieldDamage;
                this.CurrentState.OnShieldChange(remainShield, ShieldReason.DAMAGE);
            }
            this.CurrentState.OnDamaged(trueDamage, reason);
        }

        public void Heal(float amount, HealReason reason, bool exceedMaxHealth = false)
        {
            this.CurrentState.OnHealed(amount, reason, exceedMaxHealth);
        }
    }
}

