using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState _currentState = null;

    // Start is called before the first frame update
    void Start()
    {
        this._currentState = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (this._currentState != null)
        {
            this._currentState.HandleInput();
            this._currentState.LogicUpdate();
        }
    }

    void FixedUpdate()
    {
        if (this._currentState != null)
        {
            this._currentState.PhysicUpdate();
        }
    }

    public void Init(BaseState startState)
    {
        this._currentState = startState;
        this._currentState.OnEnter();
    }

    public void ChangeState(BaseState newState)
    {
        if (newState != this._currentState)
        {
            this._currentState.OnExit();
            this._currentState = newState;
            newState.OnEnter();
        }
    }
}
