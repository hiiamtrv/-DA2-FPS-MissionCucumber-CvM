using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubData;

namespace Character
{
    namespace HealthState
    {
        public class Base : BaseState
        {
            protected HealthEngine StateMachine => this._stateMachine as HealthEngine;
            protected HealthModel Model => this.StateMachine.Model;

            protected float Health { get => this.Model.Health; set => this.Model.Health = value; }
            protected float HealthRegen { get => this.Model.HealthRegen; set => this.Model.HealthRegen = value; }
            protected float Shield { get => this.Model.Shield; set => this.Model.Shield = value; }

            public Base(StateMachine stateMachine) : base(stateMachine) { }

            public override void LateUpdate()
            {
                if (this != null && Health <= 0)
                {
                    this.OnHealthOut();
                }
            }

            public virtual void OnShieldChange(float amount, ShieldReason reason)
            {
                if (amount >= 0)
                {
                    if (amount == 0) this.OnShieldOut();
                    this.Shield = amount;
                    EventCenter.Publish(
                        EventId.SHILED_CHANGE,
                        new ShieldChange(this._gameObject, this.Shield, reason)
                    );
                }
                //TODO: Override if you want some effect before/after get shield damage
            }

            public virtual void OnHealed(float amount, HealReason reason, bool exceedMaxHealth)
            {
                //TODO: Override if you want some effect before/after get healed
                this.Health += amount;
                if (exceedMaxHealth) this.Health = Mathf.Max(this.Health, this.Model.MaxHealth);
                EventCenter.Publish(
                    EventId.HEALTH_CHANGE,
                    new HealthChange(this._gameObject, this.Health, reason)
                );
            }

            public virtual void OnDamaged(float damage, DamageReason reason)
            {
                //TODO: Override if you want some effect before/after take damage
                this.Health -= damage;
                Debug.LogFormat("{0} get damage {1}, remain health {2}", this.StateMachine, damage, this.Health);
                EventCenter.Publish(
                    EventId.HEALTH_CHANGE,
                    new HealthChange(this._gameObject, this.Health, reason)
                );
            }

            protected virtual void OnHealthOut()
            {
                //TODO: Override if you want some effect before/after death (as default)
                // this.StateMachine.Destroy();
                this._gameObject.SetActive(false);
                EventCenter.Publish(EventId.CHARACTER_ELIMINATED);
            }

            protected virtual void OnShieldOut()
            {
                //TODO: Override if you want some effect before/after out of shield (as default)
            }
        }
    }
}