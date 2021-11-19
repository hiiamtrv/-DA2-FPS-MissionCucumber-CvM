using UnityEngine;

namespace PubData
{
    public class WeaponAmmoChange
    {
        GameObject _dispatcher;
        int _remainAmmo;
        int _totalAmmo;

        public GameObject Dispatcher => this._dispatcher;
        public int RemainAmmo => this._remainAmmo;
        public int TotalAmmo => this._totalAmmo;

        public WeaponAmmoChange(GameObject dispatcher, int remainAmmo, int totalAmmo)
        {
            this._dispatcher = dispatcher;
            this._remainAmmo = remainAmmo;
            this._totalAmmo = totalAmmo;
        }
    }
}