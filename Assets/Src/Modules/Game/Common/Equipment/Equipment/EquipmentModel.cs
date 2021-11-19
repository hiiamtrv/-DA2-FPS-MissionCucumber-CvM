using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipments
{
    public class EquipmentModel
    {
        float _baseEquipTime;

        float _equipTime;

        public EquipmentModel(float equipTime)
        {
            this._baseEquipTime = equipTime;

            this._equipTime = this._baseEquipTime;
        }

        public float EquipTime { get => this._equipTime; set => this._equipTime = value; }

        public float EquipTimePercent
        {
            get => this._equipTime / this._baseEquipTime;
            set => this._equipTime = this._baseEquipTime * value;
        }
    }
}