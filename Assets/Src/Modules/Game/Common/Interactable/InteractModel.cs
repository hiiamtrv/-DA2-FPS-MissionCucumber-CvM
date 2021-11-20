using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class InteractModel
    {
        float _interactTime;
        bool _canMoveWhileInteract;

        public InteractModel(float interactTime, bool canMoveWhileInteract)
        {
            this._interactTime = interactTime;
            this._canMoveWhileInteract = canMoveWhileInteract;
        }

        public float InteractTime { get => this._interactTime; set => this._interactTime = value; }
        public bool CanMoveWhileInteract => this._canMoveWhileInteract;
    }
}
