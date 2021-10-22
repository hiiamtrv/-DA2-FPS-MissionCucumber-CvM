using UnityEngine;

public class BaseState
{
    protected GameObject _gameObject;
    protected StateMachine _stateMachine;

    public BaseState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _gameObject = stateMachine.gameObject;
    }


    #region NEXT STATE
    BaseState _nextState = null;
    public void SetNextState(BaseState nextState)
    {
        this._nextState = nextState;
    }

    public BaseState GetNextState()
    {
        if (this._nextState == null)
        {
            this.CheckNextState();
        }
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

    public virtual void LogicUpdate()
    {
        //OVERRIDE ME !
    }

    public virtual void PhysicUpdate()
    {
        //OVERRIDE ME !
    }

    protected virtual void CheckNextState()
    {
        //OVERRIDE ME !
    }
}