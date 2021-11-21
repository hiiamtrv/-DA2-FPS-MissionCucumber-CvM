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
    }
}