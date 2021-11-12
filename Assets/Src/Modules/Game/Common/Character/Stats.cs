using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Stats : MonoBehaviour
    {
        public float Speed;
        public float JumpHeight;
        public float RotateSpeed;

        MoveModel _moveModel;
        public MoveModel MoveModel => this._moveModel;

        RotateModel _rotateModel;
        public RotateModel RotateModel => this._rotateModel;

        void Awake()
        {
            this._moveModel = new MoveModel(Speed, JumpHeight);
            this._rotateModel = new RotateModel(RotateSpeed);
        }
    }
}