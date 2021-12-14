using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace FireBall
{
    public class FireballStats : UtilityStats
    {
        public float _damage;

        FireballModel _model;
        public FireballModel Model => this._model;

        void Awake()
        {
            this._model = new FireballModel(_equipTime, _cooldown, _enableCrosshair, _damage);
        }
    }
}