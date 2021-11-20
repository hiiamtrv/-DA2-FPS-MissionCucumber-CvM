using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class InteractableStats : MonoBehaviour
    {
        public float interactTime;
        public bool canMoveWhileInteract;

        InteractModel _model;
        public InteractModel Model => this._model;

        void Awake()
        {
            this._model = new InteractModel(interactTime, canMoveWhileInteract);
        }
    }
}