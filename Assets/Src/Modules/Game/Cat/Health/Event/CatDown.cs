using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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

        public object Serialize()
        {
            return new object[]{
                this._dispatcher.GetComponent<PhotonView>().ViewID,
                this._downtime
            };
        }

        public static object Deserialize(object[] data)
        {
            GameObject dispatcher = PhotonView.Find((int)data[0]).gameObject;
            float downTime = (float)data[1];
            return new CatDown(dispatcher, downTime);
        }
    }
}
