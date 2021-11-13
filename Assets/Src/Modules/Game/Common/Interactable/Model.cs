using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class Model
    {
        float _interactTime;
        bool _canMoveWhileInteract;

        public Model(float interactTime, bool canMoveWhileInteract)
        {
            this._interactTime = interactTime;
            this._canMoveWhileInteract = canMoveWhileInteract;
        }

        public float InteractTime { get => this._interactTime; set => this._interactTime = value; }
        public bool CanMoveWhileInteract => this._canMoveWhileInteract;
    }
}
