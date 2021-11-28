using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace PubData
{
    public class WeaponAmmoEquip
    {
        GameObject _dispatcher;
        float _ammoInMag;
        float _ammoRemain;

        public GameObject Dispatcher => this._dispatcher;
        public float AmmoInMag => this._ammoInMag;
        public float AmmoRemain => this._ammoRemain;

        public WeaponAmmoEquip(GameObject dispatcher, float ammoInMag, float ammoRemain)
        {
            this._dispatcher = dispatcher;
            this._ammoInMag = ammoInMag;
            this._ammoRemain = ammoRemain;
        }
    }
}
