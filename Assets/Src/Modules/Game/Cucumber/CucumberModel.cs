using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable;

namespace Cucumbers
{
    public class CucumberModel : InteractModel
    {
        float _baseCatInteractTime;
        float _baseMouseInteractTime;

        float _catInteractTime;
        float _mouseInteractTime;

        public CucumberModel(float catInteractTime, float mouseInteractTime, bool canMoveWhileInteract, float interactRadius)
            : base(0, canMoveWhileInteract, interactRadius)
        {
            this._baseCatInteractTime = catInteractTime;
            this._baseMouseInteractTime = mouseInteractTime;

            this._catInteractTime = this._baseCatInteractTime;
            this._mouseInteractTime = this._baseMouseInteractTime;
        }

        public float CatInteractTime { get => this._catInteractTime; set => this._catInteractTime = value; }
        public float MouseInteractTime { get => this._mouseInteractTime; set => this._mouseInteractTime = value; }

        public float CatInteractTimePercent
        {
            get => this._catInteractTime / this._baseCatInteractTime;
            set => this._catInteractTime = this._baseCatInteractTime * value;
        }

        public float MouseInteractTimePercent
        {
            get => this._mouseInteractTime / this._baseMouseInteractTime;
            set => this._mouseInteractTime = this._baseMouseInteractTime * value;
        }

        public float GetInteractTime(CharacterSide side)
        {
            switch (side)
            {
                case CharacterSide.CATS: return this._catInteractTime;
                case CharacterSide.MICE: return this._mouseInteractTime;
                default: return 99;
            }
        }
    }
}
