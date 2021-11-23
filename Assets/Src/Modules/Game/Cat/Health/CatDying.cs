using System.Collections;
using System.Collections.Generic;
using Character;
using Character.HealthState;
using UnityEngine;

namespace Cats
{
    namespace HealthState
    {
        public class CatDying : Base
        {
            public CatDying(StateMachine stateMachine) : base(stateMachine) { }

            CatDyingModel _catDyingModel;
            public CatDyingModel CatDyingModel => this._catDyingModel;

            float _lastEffectTime;
            public float LastEffectTime => this._lastEffectTime;

            public override void OnEnter()
            {
                CatDyingStats stats = this._gameObject.GetComponent<CatDyingStats>();
                if (stats != null)
                {
                    this.Model.Shield = 0;
                    this._catDyingModel = stats.Model;
                    this._lastEffectTime = Time.time;
                }
                else
                {
                    Debug.LogWarningFormat("[{0}] Cannot get CatDyingStats", this._gameObject);
                    this.SetNextState(new Base(this.StateMachine));
                }
            }

            public override void LateUpdate()
            {
                float interval = Time.time - this._lastEffectTime;
                if (interval >= this.CatDyingModel.Interval)
                {
                    this._lastEffectTime = Time.time;
                    float damage = this.CatDyingModel.Amount;
                    this.OnDamaged(damage, DamageReason.CAT_DYING);
                }
                base.LateUpdate();
            }

            public override void OnDamaged(float damage, DamageReason reason)
            {
                if (reason == DamageReason.CAT_DYING) base.OnDamaged(damage, reason);
            }

            public override void OnShieldChange(float amount, ShieldReason reason)
            {
                //do nothing
            }

            protected override void OnShieldOut()
            {
                //do nothing
            }
        }
    }
}