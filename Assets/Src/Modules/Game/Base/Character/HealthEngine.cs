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

        public void GainShield(float amount, ShieldReason reason = ShieldReason.DEFAULT, GameObject sender = null)
        {
            this.CurrentState.OnShieldChange(amount, reason, sender);
        }

        public void InflictDamage(float damage, DamageReason reason = DamageReason.DEFAULT, float penetration = 0, GameObject sender = null)
        {
            float shieldDamage = Mathf.Min(damage * (1 - penetration), this.Model.Shield);
            float trueDamage = damage - shieldDamage;
            if (this.Model.Shield > 0)
            {
                float remainShield = this.Model.Shield - shieldDamage;
                this.CurrentState.OnShieldChange(remainShield, ShieldReason.DAMAGE, sender);
            }
            this.CurrentState.OnDamaged(trueDamage, reason, sender);

            if (GameVar.Ins.SelfDamage && sender != null) {
                HealthEngine health = sender.GetComponent<HealthEngine>();
                if (health != null) health.InflictDamage(damage, DamageReason.SELF_DAMAGE, penetration, null);
            }
        }

        public void Heal(float amount, HealReason reason, bool exceedMaxHealth = false, GameObject sender = null)
        {
            this.CurrentState.OnHealed(amount, reason, exceedMaxHealth, sender);
        }
    }
}

