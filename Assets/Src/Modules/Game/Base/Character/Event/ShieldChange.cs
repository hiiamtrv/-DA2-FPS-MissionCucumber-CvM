using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using Photon.Pun;

namespace PubData
{
    public class ShieldChange
    {
        GameObject _dispatcher;
        float _newShield;
        ShieldReason _reason;

        public GameObject Dispatcher => this._dispatcher;
        public float NewShield => this._newShield;
        public ShieldReason Reason => this._reason;

        public ShieldChange(GameObject dispatcher, float newShield, ShieldReason reason)
        {
            this._dispatcher = dispatcher;
            this._newShield = newShield;
            this._reason = reason;
        }

        public object Serialize()
        {
            return new object[]{
                this._dispatcher.GetComponent<PhotonView>().ViewID,
                this._newShield,
                (int)this._reason
            };
        }

        public static object Deserialize(object[] data)
        {
            GameObject dispatcher = PhotonView.Find((int)data[0]).gameObject;
            float newShield = (float)data[1];
            ShieldReason reason = (ShieldReason)((int)data[2]);
            return new ShieldChange(dispatcher, newShield, reason);
        }
    }
}
