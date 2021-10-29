using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected BaseState _currentState = null;

    bool _didLogicUpdate = false;
    bool _didPhysicUpdate = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //State machine would start at default state
        var defaultState = this.GetDefaultState();
        if (defaultState != null)
        {
            this.Init(defaultState);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (this._currentState != null)
        {
            this._currentState.LogicUpdate();
            this.FlagUpdate(true, null);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (this._currentState != null)
        {
            this._currentState.PhysicUpdate();
            this.FlagUpdate(null, true);
        }
    }

    public void Init(BaseState startState)
    {
        this._currentState = startState;
        this._currentState.OnEnter();
    }

    public void ChangeState(BaseState newState)
    {
        if (newState != null && this._currentState != null && newState != this._currentState)
        {
            this._currentState.OnExit();
            this._currentState = newState;
            newState.OnEnter();
        }
    }

    public virtual BaseState GetDefaultState() => null;

    void FlagUpdate(bool? logicUpdate, bool? physicUpdate)
    {
        if (logicUpdate != null) this._didLogicUpdate |= (bool)logicUpdate;
        if (physicUpdate != null) this._didPhysicUpdate |= (bool)physicUpdate;
        if (this._didLogicUpdate && this._didPhysicUpdate)
        {
            BaseState nextState = this._currentState.GetNextState();
            this.ChangeState(nextState);
            this._didLogicUpdate = false;
            this._didPhysicUpdate = false;
        }
    }

    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }
}
