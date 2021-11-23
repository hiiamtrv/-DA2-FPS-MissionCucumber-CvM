using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipments
{
    public class EquipmentStats : MonoBehaviour
    {
        public float _equipTime;

        EquipmentModel _model;
        public EquipmentModel Model => this._model;

        void Awake()
        {
            this._model = new EquipmentModel(_equipTime);
        }
    }
}
