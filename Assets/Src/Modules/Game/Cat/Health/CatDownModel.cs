using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats
{
    public class CatDownModel
    {
        float _baseTimeDown;
        float _baseSlow;
        float _baseShieldReceived;

        float _timeDown;
        float _slow;
        float _shieldReceived;

        public CatDownModel(float timeDown, float slow, float shieldReceived)
        {
            this._baseTimeDown = timeDown;
            this._baseSlow = slow;
            this._baseShieldReceived = shieldReceived;

            this._timeDown = this._baseTimeDown;
            this._slow = this._baseSlow;
            this._shieldReceived = this._baseShieldReceived;
        }

        public float TimeDown { get => this._timeDown; set => this._timeDown = value; }
        public float Slow { get => this._slow; set => this._slow = value; }
        public float ShieldReceived { get => this._shieldReceived; set => this._shieldReceived = value; }

        public float TimeDownPercent
        {
            get => this._timeDown / this._baseTimeDown;
            set => this._timeDown = this._baseTimeDown * value;
        }

        public float SlowPercent
        {
            get => this._slow / this._baseSlow;
            set => this._slow = this._baseSlow * value;
        }

        public float ShieldReceivedPercent
        {
            get => this._shieldReceived / this._baseShieldReceived;
            set => this._shieldReceived = this._baseShieldReceived * value;
        }
    }

}
