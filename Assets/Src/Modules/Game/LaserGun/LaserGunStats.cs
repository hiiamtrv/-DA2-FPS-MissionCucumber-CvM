using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace LaserGun
{
    public class LaserGunStats : AmmoWeaponStats
    {
        const float SHOT_RANGE = (float)(1E13 + 7);

        LaserGunModel _model;
        public LaserGunModel Model => this._model;

        void Awake()
        {
            this._shotRange = SHOT_RANGE;
            this._model = new LaserGunModel(_equipTime, _fireRate, _damage, _shotRange, _shotSpread, _reloadSpeed, _magazineSize, _maxAmmo);
        }
    }
}
