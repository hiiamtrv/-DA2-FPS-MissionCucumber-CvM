using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats
{
    public class CatDyingStats : MonoBehaviour
    {
        [SerializeField] float _interval;
        [SerializeField] float _amount;

        CatDyingModel _model;
        public CatDyingModel Model => this._model;

        void Awake()
        {
            this._model = new CatDyingModel(this._interval, this._amount);
        }
    }
}