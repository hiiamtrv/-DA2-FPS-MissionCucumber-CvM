using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Equipments;

namespace Utilities
{
    public class UtilityStats : EquipmentStats
    {
        UtilityModel _model;
        public UtilityModel Model => this._model;

        void Awake()
        {
            this._model = new EquipmentModel(_equipTime) as UtilityModel;
        }
    }
}
