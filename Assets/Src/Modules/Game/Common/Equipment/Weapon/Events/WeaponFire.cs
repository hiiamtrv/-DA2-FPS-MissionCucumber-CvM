using UnityEngine;

namespace PubData
{
    public class WeaponFire
    {
        GameObject _dispatcher;
        int _remainAmmo;

        public GameObject Dispatcher => this._dispatcher;
        public int RemainAmmo => this._remainAmmo;

        public WeaponFire(GameObject dispatcher, int remainAmmo)
        {
            this._dispatcher = dispatcher;
            this._remainAmmo = remainAmmo;
        }
    }
}