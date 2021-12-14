using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Utilities
{
    public class UtilityModel : EquipmentModel
    {
        float _baseCooldown;

        float _cooldown;
        bool _enableCrosshair;

        public UtilityModel(float equipTime, float cooldown, bool enableCrossHair) : base(equipTime)
        {
            this._baseCooldown = cooldown;

            this._cooldown = cooldown;
            this._enableCrosshair = enableCrossHair;
        }

        public float Cooldown { get => this._cooldown; set => this._cooldown = value; }
        public bool EnableCrosshair => this._enableCrosshair;

        public float CooldownPercent
        {
            get => this._cooldown / this._baseCooldown;
            set => this._cooldown = this._baseCooldown * value;
        }
    }
}
