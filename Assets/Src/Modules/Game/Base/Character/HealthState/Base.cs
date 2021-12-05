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

            public Base(StateMachine stateMachine) : base(stateMachine)
            {
                EventCenter.Subcribe(EventId.HEALTH_CHANGE, this.HealthUpdate);
                EventCenter.Subcribe(EventId.SHILED_CHANGE, this.ShieldUpdate);
            }

            public override void LateUpdate()
            {
                if (this != null && Health <= 0)
                {
                    this.OnHealthOut();
                }
            }

            public virtual void OnShieldChange(float amount, ShieldReason reason, GameObject sender)
            {
                if (amount >= 0)
                {
                    this.ActOnReason(reason);
                    if (amount == 0) this.OnShieldOut();
                    this.Shield = amount;
                    ShieldChange pubData = new ShieldChange(this._gameObject, this.Shield, reason);
                    EventCenter.Publish(EventId.SHILED_CHANGE, pubData);
                    Debug.Log("Check null: ", pubData == null, pubData, pubData.Dispatcher, pubData.NewShield, pubData.Reason);
                    Debug.Log("Check serialize: ", pubData.Serialize().ToString());
                    NetworkGame.Publish(EventId.SHILED_CHANGE, pubData.Serialize());
                }
                //TODO: Override if you want some effect before/after get shield damage
            }

            public virtual void OnHealed(float amount, HealReason reason, bool exceedMaxHealth, GameObject sender)
            {
                //TODO: Override if you want some effect before/after get healed
                this.Health += amount;
                if (exceedMaxHealth) this.Health = Mathf.Max(this.Health, this.Model.MaxHealth);

                HealthChange pubData = new HealthChange(this._gameObject, this.Health, reason);
                EventCenter.Publish(EventId.HEALTH_CHANGE, pubData);
                NetworkGame.Publish(EventId.HEALTH_CHANGE, pubData.Serialize());
                this.ActOnReason(reason);
            }

            public virtual void OnDamaged(float damage, DamageReason reason, GameObject sender)
            {
                //TODO: Override if you want some effect before/after take damage
                this.Health -= damage;
                UnityEngine.Debug.LogFormat("{0} get damage {1}, remain health {2}", this.StateMachine, damage, this.Health);

                HealthChange pubData = new HealthChange(this._gameObject, this.Health, reason);
                EventCenter.Publish(EventId.HEALTH_CHANGE, pubData);
                NetworkGame.Publish(EventId.HEALTH_CHANGE, pubData.Serialize());
                this.ActOnReason(reason);
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

            protected virtual void HealthUpdate(object pubData)
            {
                HealthChange data = pubData as HealthChange;
                if (data.Dispatcher == this._gameObject)
                {
                    this.Health = data.NewHealth;
                    if (data.IsDamageEvent) this.ActOnReason(data.DamageReason);
                    else if (data.IsHealEvent) this.ActOnReason(data.HealReason);
                }
            }

            protected virtual void ShieldUpdate(object pubData)
            {
                ShieldChange data = pubData as ShieldChange;
                if (data.Dispatcher == this._gameObject)
                {
                    this.Shield = data.NewShield;
                    this.ActOnReason(data.Reason);
                }
            }

            protected virtual void ActOnReason(HealReason reason)
            {
                //TODO: Override
            }

            protected virtual void ActOnReason(DamageReason reason)
            {
                //TODO: Override
            }

            protected virtual void ActOnReason(ShieldReason reason)
            {

                //TODO: Override
            }
        }
    }
}