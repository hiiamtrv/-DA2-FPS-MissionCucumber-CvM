using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterStats : MonoBehaviour
    {
        public float Speed;
        public float JumpHeight;
        public float RotateSpeed;

        public uint Health;
        public uint HealthRegen;
        public uint Shield;

        MoveModel _moveModel;
        public MoveModel MoveModel => this._moveModel;

        RotateModel _rotateModel;
        public RotateModel RotateModel => this._rotateModel;

        HealthModel _healthModel;
        public HealthModel HealthModel => this._healthModel;

        void Awake()
        {
            this._moveModel = new MoveModel(Speed, JumpHeight);
            this._rotateModel = new RotateModel(RotateSpeed);
            this._healthModel = new HealthModel(Health, HealthRegen, Shield);
        }
    }
}