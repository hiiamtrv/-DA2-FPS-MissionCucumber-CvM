using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats
{
    public class CatDownStats : MonoBehaviour
    {
        [SerializeField] float _timeDown;
        [SerializeField] float _slow;
        [SerializeField] float _shieldReceived;

        CatDownModel _model;
        public CatDownModel Model => this._model;

        void Awake()
        {
            this._model = new CatDownModel(this._timeDown, this._slow, this._shieldReceived);
        }
    }
}