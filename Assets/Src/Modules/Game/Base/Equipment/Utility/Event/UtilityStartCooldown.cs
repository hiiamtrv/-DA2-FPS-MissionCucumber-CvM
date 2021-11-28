using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace PubData
{
    public class UtilityStartCooldown
    {
        GameObject _dispatcher;
        float _cooldown;

        public GameObject Dispatcher => this._dispatcher;
        public float Cooldown => this._cooldown;

        public UtilityStartCooldown(GameObject dispatcher, float cooldown)
        {
            this._dispatcher = dispatcher;
            this._cooldown = cooldown;
        }
    }
}
