using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MoveModel
    {
        float _baseSpeed;
        float _baseJumpHeight;

        float _speed;
        float _jumpHeight;

        float _moveX;
        float _moveY;
        float _moveZ;

        public MoveModel(float baseSpeed, float baseJumpHeight)
        {
            this._baseSpeed = baseSpeed;
            this._baseJumpHeight = baseJumpHeight;

            this._speed = this._baseSpeed;
            this._jumpHeight = this._baseJumpHeight;
            this._moveX = 0;
            this._moveY = 0;
            this._moveZ = 0;
        }

        public float MoveX { get => this._moveX; set => this._moveX = value; }
        public float MoveY { get => this._moveY; set => this._moveY = value; }
        public float MoveZ { get => this._moveZ; set => this._moveZ = value; }

        public float Speed { get => this._speed; set => this._speed = value; }
        public float JumpHeight { get => this._jumpHeight; set => this._jumpHeight = value; }

        public float SpeedPercent
        {
            get => this._speed / this._baseSpeed;
            set => this._speed = this._baseSpeed * value;
        }

        public float JumpHeightPercent
        {
            get => this._jumpHeight / this._baseJumpHeight;
            set => this._jumpHeight = this._baseJumpHeight * value;
        }
    }
}
