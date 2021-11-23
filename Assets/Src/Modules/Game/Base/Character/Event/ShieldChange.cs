using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

namespace PubData
{
    public class ShieldChange
    {
        GameObject _dispatcher;
        float _amount;
        ShieldReason _reason;

        public GameObject Dispatcher => this._dispatcher;
        public float Amount => this._amount;
        public ShieldReason Reason => this._reason;

        public ShieldChange(GameObject dispatcher, float amount, ShieldReason reason)
        {
            this._dispatcher = dispatcher;
            this._amount = amount;
            this._reason = reason;
        }
    }
}
