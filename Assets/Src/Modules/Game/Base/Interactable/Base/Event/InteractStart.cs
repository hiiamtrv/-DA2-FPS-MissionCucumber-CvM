using Interactable;
using Photon.Pun;
using UnityEngine;

namespace PubData
{
    public class IneractStart
    {
        GameObject _dispatcher;
        float _startTime;
        InteractModel _interactModel;
        GameObject _interactObject;

        public GameObject Dispatcher => this._dispatcher;
        public float StartTime => this._startTime;
        public InteractModel InteractModel => this._interactModel;
        public GameObject InteractObject => this._interactObject;

        public IneractStart(GameObject dispatcher, float startTime, GameObject interactObject, InteractModel interactModel)
        {
            this._dispatcher = dispatcher;
            this._startTime = startTime;
            this._interactObject = interactObject;
            this._interactModel = interactModel;
        }
    }
}