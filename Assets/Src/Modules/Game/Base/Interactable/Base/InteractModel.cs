using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class InteractModel
    {
        protected float _baseInteractTime;
        protected float _baseInteractRadius;

        protected float _interactTime;
        protected bool _canMoveWhileInteract;
        protected float _interactRadius;

        public InteractModel(float interactTime, bool canMoveWhileInteract, float interactRadius)
        {
            this._baseInteractTime = interactTime;
            this._canMoveWhileInteract = canMoveWhileInteract;
            this._baseInteractRadius = interactRadius;

            this._interactTime = this._baseInteractTime;
            this._interactRadius = this._baseInteractRadius;
        }

        public float InteractTime { get => this._interactTime; set => this._interactTime = value; }
        public bool CanMoveWhileInteract => this._canMoveWhileInteract;
        public float InteractRadius { get => this._interactRadius; set => this._interactRadius = value; }

        public float InteractTimePercent
        {
            get => this._interactTime / this._baseInteractTime;
            set => this._interactTime = this._baseInteractTime * value;
        }

        public float InteractRadiusPercent
        {
            get => this._interactRadius / this._baseInteractRadius;
            set => this._interactRadius = this._baseInteractRadius * value;
        }
    }
}
