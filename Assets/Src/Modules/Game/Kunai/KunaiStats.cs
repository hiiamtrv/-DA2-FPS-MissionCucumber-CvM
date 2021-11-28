using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectile;
using Weapons;
using System;

namespace Kunai
{
    public class KunaiStats : AmmoWeaponStats
    {
        const float SHOT_RANGE = (float)(1E13 + 7);

        KunaiModel _model;
        public KunaiModel Model => this._model;

        void Awake()
        {
            this._shotRange = SHOT_RANGE;
            this._model = new KunaiModel(_equipTime, _fireRate, _damage, _shotRange, _shotSpread, _reloadSpeed, _magazineSize, _maxAmmo);
        }
    }
}
