using Interactable;
using Photon.Pun;
using UnityEngine;

namespace PubData
{
    public class IneractStart
    {
        GameObject _dispatcher;
        float _startTime;
        float _interactTime;
        GameObject _interactObject;

        public GameObject Dispatcher => this._dispatcher;
        public float StartTime => this._startTime;
        public float InteractTime => this._interactTime;
        public GameObject InteractObject => this._interactObject;

        public IneractStart(GameObject dispatcher, float startTime, GameObject interactObject, float interactTime)
        {
            this._dispatcher = dispatcher;
            this._startTime = startTime;
            this._interactObject = interactObject;
            this._interactTime = interactTime;
        }

        public object Serialize()
        {
            return new object[]{
                this._dispatcher.GetComponent<PhotonView>().ViewID,
                this._startTime,
                this._interactObject.GetComponent<PhotonView>().ViewID,
                this._interactTime
            };
        }

        public static object Deserialize(object[] data)
        {
            GameObject dispatcher = PhotonView.Find((int)data[0]).gameObject;
            float startTime = (float)data[1];
            GameObject interactObject = PhotonView.Find((int)data[2]).gameObject;
            float interactTime = (float)data[3];
            return new IneractStart(dispatcher, startTime, interactObject, interactTime);
        }
    }
}