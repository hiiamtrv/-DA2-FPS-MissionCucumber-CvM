using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace PubData
{
    public class WeaponUnequip
    {
        GameObject _dispatcher;

        public GameObject Dispatcher => this._dispatcher;

        public WeaponUnequip(GameObject dispatcher)
        {
            this._dispatcher = dispatcher;
        }
    }
}
