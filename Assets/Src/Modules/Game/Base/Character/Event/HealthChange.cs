using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace PubData
{
    public class HealthChange
    {
        GameObject _dispatcher;
        float _amount;
        DamageReason? _damageReason = null;
        HealReason? _healReason = null;

        public GameObject Dispatcher => this._dispatcher;
        public float Amount => this._amount;
        public DamageReason? DamageReason => this._damageReason;
        public HealReason? HealReason => this._healReason;

        public bool IsHealEvent => this.HealReason != null;
        public bool IsDamageEvent => this.DamageReason != null;

        public HealthChange(GameObject dispatcher, float amount, DamageReason damageReason)
        {
            this._dispatcher = dispatcher;
            this._amount = amount;
            this._damageReason = damageReason;
        } 
        
        public HealthChange(GameObject dispatcher, float amount, HealReason healReason)
        {
            this._dispatcher = dispatcher;
            this._amount = amount;
            this._healReason = healReason;
        }
    }
}
