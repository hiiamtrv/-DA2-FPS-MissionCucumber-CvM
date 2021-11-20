using Interactable;
using UnityEngine;

namespace PubData
{
    public class IneractStart
    {
        GameObject _dispatcher;
        float _startTime;
        InteractModel _interactModel;

        public GameObject Dispatcher => this._dispatcher;
        public float StartTime => this._startTime;
        public InteractModel InteractModel => this._interactModel;

        public IneractStart(GameObject dispatcher, float startTime, InteractModel interactModel)
        {
            this._dispatcher = dispatcher;
            this._startTime = startTime;
            this._interactModel = interactModel;
        }
    }
}