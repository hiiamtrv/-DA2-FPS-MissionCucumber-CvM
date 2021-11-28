using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable;

namespace Cucumbers
{
    public class CucumberStats : InteractableStats
    {
        [SerializeField] protected float _catObtainTime;
        [SerializeField] protected float _mouseObtainTime;

        CucumberModel _model;
        public CucumberModel Model => this._model;

        void Awake()
        {
            this._model = new CucumberModel(_catObtainTime, _mouseObtainTime, _canMoveWhileInteract, _interactRadius);
        }
    }
}
