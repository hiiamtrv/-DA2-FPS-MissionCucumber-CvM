using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class HealthModel
    {
        float _baseHealth;
        float _baseHealthRegen;
        float _baseShield;

        float _health;
        float _healthRegen;
        float _shield;

        public HealthModel(uint health, uint healthRegen, uint shield)
        {
            this._baseHealth = health;
            this._baseHealthRegen = healthRegen;
            this._baseShield = shield;

            this._health = this._baseHealth;
            this._healthRegen = this._baseHealthRegen;
            this._shield = this._baseShield;
        }

        public float MaxHealth => this._baseHealth;
        public float Health { get => this._health; set => this._health = value; }
        public float HealthRegen { get => this._healthRegen; set => this._healthRegen = value; }
        public float Shield { get => this._shield; set => this._shield = value; }

        public float HealthPercent
        {
            get => this._health / this._baseHealth;
            set => this._health = this._baseHealth * value;
        }

        public float HealthRegenPercent
        {
            get => this._healthRegen / this._baseHealthRegen;
            set => this._healthRegen = this._baseHealthRegen * value;
        }

        public float ShieldPercent
        {
            get => this._shield / this._baseShield;
            set => this._shield = this._baseShield * value;
        }
    }

    public enum DamageReason
    {
        NULL,
        DEFAULT,
        CAT_DYING,
        SELF_DAMAGE,
    }

    public enum HealReason
    {
        NULL,
        REGEN,
        SELF_SKILL,
        ALLIES_SKILL,
    }

    public enum ShieldReason
    {
        DEFAULT,
        DAMAGE,
        CAT_RECOVERED,
        CAT_WEAKENED,
        FORCE_SET,
    }
}
