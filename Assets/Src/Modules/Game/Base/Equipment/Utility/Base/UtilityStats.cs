using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Equipments;

namespace Utilities
{
    public class UtilityStats : EquipmentStats
    {
        [SerializeField] protected float _cooldown;

        UtilityModel _model;
        public UtilityModel Model => this._model;

        void Awake()
        {
            this._model = new UtilityModel(_equipTime, _cooldown);
        }
    }
}
