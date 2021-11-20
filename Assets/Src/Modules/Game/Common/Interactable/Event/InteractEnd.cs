using UnityEngine;

namespace PubData
{
    public class InteractEnd
    {
        GameObject _dispatcher;
        float _interactTime;

        public GameObject Dispatcher => this._dispatcher;

        public InteractEnd(GameObject dispatcher)
        {
            this._dispatcher = dispatcher;
        }
    }
}