using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeaponStats : EquipmentStats
    {
        public float _attackSpeed;
        public float _damage;
        public float _attackRange;

        MeleeWeaponModel _model;
        public MeleeWeaponModel Model => this._model;

        void Awake()
        {
            this._model = new MeleeWeaponModel(_equipTime, _attackSpeed, _damage, _attackRange);
        }
    }
}
