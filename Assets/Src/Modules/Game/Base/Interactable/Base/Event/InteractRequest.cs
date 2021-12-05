using Interactable;
using Photon.Pun;
using UnityEngine;

namespace PubData
{
    public class InteractRequest
    {
        GameObject _dispatcher;
        GameObject _interactObject;

        public GameObject Dispatcher => this._dispatcher;
        public GameObject InteractObject => this._interactObject;

        public InteractRequest(GameObject dispatcher, GameObject interactObject)
        {
            this._dispatcher = dispatcher;
            this._interactObject = interactObject;
        }
        public object Serialize()
        {
            return new object[]{
                this._dispatcher.GetComponent<PhotonView>().ViewID,
                this._interactObject.GetComponent<PhotonView>().ViewID,
            };
        }

        public static object Deserialize(object[] data)
        {
            GameObject dispatcher = PhotonView.Find((int)data[0]).gameObject;
            GameObject interactObject = PhotonView.Find((int)data[1]).gameObject;
            return new InteractRequest(dispatcher, interactObject);
        }
    }
}