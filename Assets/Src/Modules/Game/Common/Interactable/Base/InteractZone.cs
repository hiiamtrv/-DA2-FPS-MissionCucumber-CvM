using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class InteractZone : MonoBehaviour
    {
        InteractEngine _interactEngine;

        // Start is called before the first frame update
        void Start()
        {
            this._interactEngine = this.GetComponentInParent<InteractEngine>();
        }

        void OnTriggerEnter(Collider other) => this._interactEngine.NotifyObjectEnter(other.gameObject);

        void OnTriggerExit(Collider other) => this._interactEngine.NotifyObjectExit(other.gameObject);
    }
}