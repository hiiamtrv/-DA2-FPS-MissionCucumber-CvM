using UnityEngine;

public class BaseState
{
    GameObject _controller;
    StateMachine _stateMachine;

    public BaseState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }


    #region NEXT STATE
    BaseState _nextState = null;
    public void SetNextState(BaseState nextState)
    {
        this._nextState = nextState;
    }

    public BaseState GetNextState()
    {
        return (this._nextState != null ? this._nextState : this);
    }
    #endregion

    public virtual void OnEnter()
    {
        //OVERRIDE ME !
    }

    public virtual void OnExit()
    {
        //OVERRIDE ME !
    }

    #region HANDLE INPUT
    protected float zMove = 0;
    protected float xMove = 0;
    public virtual void HandleInput()
    {
        xMove = Input.GetAxis("horizontal");
        zMove = Input.GetAxis("vertical");
    }
    #endregion

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicUpdate()
    {

    }
}