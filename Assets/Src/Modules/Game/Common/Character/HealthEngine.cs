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

        protected new HealthState.Base _currentState = null;

        protected override void Start()
        {
            this.Model = this.GetComponent<CharacterStats>().HealthModel;
            base.Start();
        }

        public override BaseState GetDefaultState() => new HealthState.Base(this);

        public void OnHealthOut()
        {

        }

        public void OnShieldOut()
        {

        }

        public void GainShield(float amount, ShieldReason reason = ShieldReason.DEFAULT)
        {
            this._currentState.OnShieldChange(amount, reason);
        }

        public void InflictDamage(float damage, DamageReason reason = DamageReason.DEFAULT, float penetration = 0)
        {
            float shieldDamage = Mathf.Max(damage * (1 - penetration), this.Model.Shield);
            float trueDamage = damage - shieldDamage;
            if (this.Model.Shield > 0)
            {
                this._currentState.OnShieldChange(-shieldDamage, ShieldReason.DAMAGE);
            }
            this._currentState.OnDamaged(trueDamage, reason);
        }

        public void Heal(float amount, HealReason reason, bool exceedMaxHealth = false)
        {
            this._currentState.OnHealed(amount, reason, exceedMaxHealth);
        }
    }
}

