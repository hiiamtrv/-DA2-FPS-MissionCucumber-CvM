using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Weapons
{
    public class AmmoWeaponStats : EquipmentStats
    {
        public float _fireRate;
        public float _damage;
        public float _shotRange;
        public float _shotSpread;
        public float _reloadSpeed;
        public int _magazineSize;
        public int _maxAmmo;

        AmmoWeaponModel _model;
        public AmmoWeaponModel Model => this._model;

        void Awake()
        {
            this._model = new AmmoWeaponModel(_equipTime, _fireRate, _damage, _shotRange, _shotSpread, _reloadSpeed, _magazineSize, _maxAmmo);
        }
    }
}
