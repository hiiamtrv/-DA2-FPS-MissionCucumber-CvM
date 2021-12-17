using Photon.Pun;
using UnityEngine;

namespace PubData
{
    public class InteractEnd
    {
        GameObject _dispatcher;
        GameObject _interactObject;
        bool _isSuccessful;

        public GameObject Dispatcher => this._dispatcher;
        public GameObject InteractObject => this._interactObject;
        public bool IsSuccessful => this._isSuccessful;

        public InteractEnd(GameObject dispatcher, GameObject interactObject, bool isSuccessful)
        {
            this._dispatcher = dispatcher;
            this._interactObject = interactObject;
            this._isSuccessful = isSuccessful;
        }

        public object Serialize()
        {
            return new object[]{
                this._dispatcher.GetComponent<PhotonView>().ViewID,
                this._interactObject.GetComponent<PhotonView>().ViewID,
                this._isSuccessful,
            };
        }

        public static object Deserialize(object[] data)
        {
            GameObject dispatcher = PhotonView.Find((int)data[0]).gameObject;
            GameObject interactObject = PhotonView.Find((int)data[1]).gameObject;
            bool isSuccessful = (bool)data[2];

            InteractEnd deserializedObj = new InteractEnd(dispatcher, interactObject, isSuccessful);
            if (dispatcher == null || interactObject == null)
            {
                NetworkGame.AddFalseData(deserializedObj);
            }
            return deserializedObj;
        }
    }
}