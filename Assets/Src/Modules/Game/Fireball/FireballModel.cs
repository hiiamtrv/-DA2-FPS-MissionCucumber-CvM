using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace FireBall
{
    public class FireballModel : UtilityModel
    {
        float _baseDamage;
        float _damage;

        public FireballModel(float equipTime, float cooldown, float damage) : base(equipTime, cooldown)
        {
            this._baseDamage = damage;
            this._damage = this._baseDamage;
        }

        public float Damage { get => this._damage; set => this._damage = value; }

        public float DamagePercent
        {
            get => this._damage / this._baseDamage;
            set => this._damage = this._baseDamage * value;
        }
    }
}
