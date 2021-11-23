using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace PubData
{
    public class CatDown
    {
        GameObject _dispatcher;
        float _downtime;

        public GameObject Dispatcher => this._dispatcher;
        public float DownTime => this._downtime;

        public CatDown(GameObject dispatcher, float downTime)
        {
            this._dispatcher = dispatcher;
            this._downtime = downTime;
        }
    }
}
