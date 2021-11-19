using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace PubData
{
    public class WeaponEquip
    {
        GameObject _dispatcher;
        WeaponModel _weaponModel;

        public GameObject Dispatcher => this._dispatcher;
        public WeaponModel WeaponModel => this._weaponModel;

        public WeaponEquip(GameObject dispatcher, WeaponModel weaponModel)
        {
            this._dispatcher = dispatcher;
            this._weaponModel = weaponModel;
        }
    }
}
