using System.Collections;
using System.Collections.Generic;
using Character;
using Photon.Pun;
using UnityEngine;

namespace PubData
{
    public class HealthChange
    {
        GameObject _dispatcher;
        float _newHealth;
        DamageReason _damageReason;
        HealReason _healReason;

        public GameObject Dispatcher => this._dispatcher;
        public float NewHealth => this._newHealth;
        public DamageReason DamageReason => this._damageReason;
        public HealReason HealReason => this._healReason;

        public bool IsHealEvent => this.HealReason == HealReason.NULL;
        public bool IsDamageEvent => this.DamageReason == DamageReason.NULL;

        public HealthChange() { }

        public HealthChange(GameObject dispatcher, float newHealth, DamageReason damageReason)
        {
            this._dispatcher = dispatcher;
            this._newHealth = newHealth;
            this._damageReason = damageReason;
            this._healReason = HealReason.NULL;
        }

        public HealthChange(GameObject dispatcher, float newHealth, HealReason healReason)
        {
            this._dispatcher = dispatcher;
            this._newHealth = newHealth;
            this._healReason = healReason;
            this._damageReason = DamageReason.NULL;
        }

        HealthChange(GameObject dispatcher, float newHealth, HealReason healReason, DamageReason damageReason)
        {
            this._dispatcher = dispatcher;
            this._newHealth = newHealth;
            this._healReason = healReason;
            this._damageReason = damageReason;
        }

        public object Serialize()
        {
            return new object[]{
                this._dispatcher.GetComponent<PhotonView>().ViewID,
                this._newHealth,
                (int)this._healReason,
                (int)this._damageReason
            };
        }

        public static object Deserialize(object[] data)
        {
            GameObject dispatcher = PhotonView.Find((int)data[0]).gameObject;
            float newHealth = (float)data[1];
            HealReason healReason = (HealReason)((int)data[2]);
            DamageReason damageReason = (DamageReason)((int)data[3]);
            return new HealthChange(dispatcher, newHealth, healReason, damageReason);
        }
    }
}
