using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEngine : StateMachine
{
    const float SPEED = 20;
    const float JUMP_HEIGHT = 1f;

    float _speed;
    float _jumpHeight;

    float _moveX;
    float _moveY;
    float _moveZ;

    protected override void Start()
    {
        base.Start();
        this.ResetStats();
    }

    public void ResetStats()
    {
        this._speed = SPEED;
        this._jumpHeight = JUMP_HEIGHT;
    }

    #region GET_SET
    public override BaseState GetDefaultState() => new CharacterMoveState.Run(this);
    public float GetSpeed() => this._speed;
    public float GetJumpHeight() => this._jumpHeight;

    public float GetMoveX() => this._moveX;
    public float GetMoveY() => this._moveY;
    public float GetMoveZ() => this._moveZ;
    public void SetMoveX(float value) => this._moveX = value;
    public void SetMoveY(float value) => this._moveY = value;
    public void SetMoveZ(float value) => this._moveZ = value;
    #endregion
}
