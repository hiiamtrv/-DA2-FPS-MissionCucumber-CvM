using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class InteractZone : MonoBehaviour
    {
        InteractEngine _interactEngine;
        SphereCollider _collider;

        void Awake()
        {
            this._interactEngine = this.GetComponentInParent<InteractEngine>();
            this._collider = this.GetComponent<SphereCollider>();
        }

        void Update()
        {
            float radius = this._interactEngine.Model.InteractRadius;
            this._collider.radius = radius;
        }

        void OnTriggerEnter(Collider other) => this._interactEngine.NotifyObjectEnter(other.gameObject);

        void OnTriggerExit(Collider other) => this._interactEngine.NotifyObjectExit(other.gameObject);
    }
}