using UnityEngine;

namespace PubData
{
    public class WeaponAmmoChange
    {
        GameObject _dispatcher;
        int _ammoInMag;
        int _ammoRemain;

        public GameObject Dispatcher => this._dispatcher;
        public int AmmoInMag => this._ammoInMag;
        public int AmmoRemain => this._ammoRemain;

        public WeaponAmmoChange(GameObject dispatcher, int remainAmmo, int totalAmmo)
        {
            this._dispatcher = dispatcher;
            this._ammoInMag = remainAmmo;
            this._ammoRemain = totalAmmo;
        }
    }
}