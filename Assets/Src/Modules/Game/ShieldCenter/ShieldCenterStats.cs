using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable;

namespace ShieldCenter
{
    public class ShieldCenterStats : InteractableStats
    {
        [SerializeField] List<float> _checkPoints;

        ShieldCenterModel _model;
        public ShieldCenterModel Model => this._model;

        void Awake()
        {
            this._model = new ShieldCenterModel(_interactTime, _canMoveWhileInteract, _interactRadius, _checkPoints);
        }

        protected override void Start()
        {
            //do nothing
        }
    }
}