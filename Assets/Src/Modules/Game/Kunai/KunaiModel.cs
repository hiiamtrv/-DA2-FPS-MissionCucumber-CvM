using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Kunai
{
    public class KunaiModel : AmmoWeaponModel
    {
        public KunaiModel(
            float equipTime,
            float fireRate,
            float damage,
            float shotRange,
            float shotSpread,
            float reloadTime,
            int magazineSize,
            int totalAmmo
            ) : base(equipTime, fireRate, damage, shotRange, shotSpread, reloadTime, magazineSize, totalAmmo) { }
    }
}
