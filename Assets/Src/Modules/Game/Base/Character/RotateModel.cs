using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class RotateModel
    {
        float _baseRotateSpeed;
        float _rotateSpeed;
    

        public RotateModel(float _baseRotateSpeed)
        {
            this._baseRotateSpeed = _baseRotateSpeed;
            
            this._rotateSpeed = this._baseRotateSpeed;
        }

        public float RotateSpeed { get => this._rotateSpeed; set => this._rotateSpeed = value; }

        public float RotateSpeedPercent
        {
            get => this._rotateSpeed / this._baseRotateSpeed;
            set => this._rotateSpeed = this._baseRotateSpeed * value;
        }
    }
}
