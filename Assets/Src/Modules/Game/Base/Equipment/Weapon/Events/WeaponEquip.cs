using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace PubData
{
    public class WeaponAmmoEquip
    {
        GameObject _dispatcher;
        AmmoWeaponModel _weaponModel;

        public GameObject Dispatcher => this._dispatcher;
        public AmmoWeaponModel WeaponModel => this._weaponModel;

        public WeaponAmmoEquip(GameObject dispatcher, AmmoWeaponModel weaponModel)
        {
            this._dispatcher = dispatcher;
            this._weaponModel = weaponModel;
        }
    }
}
