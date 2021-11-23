using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats
{
    public class CatDyingModel
    {
        float _baseInterval;
        float _baseAmount;

        float _interval;
        float _amount;

        public CatDyingModel(float interval, float amount)
        {
            this._baseInterval = interval;
            this._baseAmount = amount;

            this._interval = this._baseInterval;
            this._amount = this._baseAmount;
        }

        public float Interval { get => this._interval; set => this._interval = value; }
        public float Amount { get => this._amount; set => this._amount = value; }

        public float IntervalPercent
        {
            get => this._interval / this._baseInterval;
            set => this._interval = this._baseInterval * value;
        }

        public float AmountPercent
        {
            get => this._amount / this._baseAmount;
            set => this._amount = this._baseAmount * value;
        }
    }

}
