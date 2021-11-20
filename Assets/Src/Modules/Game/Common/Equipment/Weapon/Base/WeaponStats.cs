using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Weapons
{
    public class WeaponStats : EquipmentStats
    {
        public float _fireRate;
        public float _damage;
        public float _shotRange;
        public float _shotSpread;
        public float _reloadSpeed;
        public int _magazineSize;
        public int _maxAmmo;

        WeaponModel _model;
        public WeaponModel Model => this._model;

        void Awake()
        {
            this._model = new WeaponModel(_equipTime, _fireRate, _damage, _shotRange, _shotSpread, _reloadSpeed, _magazineSize, _maxAmmo);
        }
    }
}
