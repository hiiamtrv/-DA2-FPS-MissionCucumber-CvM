using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    namespace HealthState
    {
        public class Base : BaseState
        {
            protected new HealthEngine _stateMachine;
            protected HealthModel Model => this._stateMachine.Model;

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
                if (amount != 0)
                {
                    if (this.Shield <= -amount) this.OnShieldOut();
                    this.Shield += amount;
                }
                //TODO: Override if you want some effect before/after get shield damage
            }

            public virtual void OnHealed(float amount, HealReason reason, bool exceedMaxHealth)
            {
                //TODO: Override if you want some effect before/after get healed
                this.Health += amount;
                if (exceedMaxHealth) this.Health = Mathf.Max(this.Health, this.Model.MaxHealth);
            }

            public virtual void OnDamaged(float damage, DamageReason reason)
            {
                //TODO: Override if you want some effect before/after take damage
                this.Health -= damage;
            }

            protected virtual void OnHealthOut()
            {
                //TODO: Override if you want some effect before/after death (as default)
                this._stateMachine.Destroy();
            }

            protected virtual void OnShieldOut()
            {
                //TODO: Override if you want some effect before/after out of shield (as default)
            }
        }
    }
}