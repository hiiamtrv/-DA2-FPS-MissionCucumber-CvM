using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class InteractableStats : MonoBehaviour
    {
        [SerializeField] protected float _interactTime;
        [SerializeField] protected bool _canMoveWhileInteract;
        [SerializeField] protected float _interactRadius;

        InteractModel _model;
        public InteractModel Model => this._model;

        void Awake()
        {
            this._model = new InteractModel(_interactTime, _canMoveWhileInteract, _interactRadius);
        }
    }
}