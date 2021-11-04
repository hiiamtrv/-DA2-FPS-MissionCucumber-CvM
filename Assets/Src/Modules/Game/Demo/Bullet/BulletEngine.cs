using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEngine : StateMachine
{
    const float SPEED = 0.05f;

    float _speed;

    protected override void Start()
    {
        base.Start();
        this.SetSpeed(SPEED);
    }

    public float GetSpeed() => this._speed;
    public void SetSpeed(float value) => this._speed = value;
    public override BaseState GetDefaultState() => new BulletState.Fly(this);
}
