using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractEngine : StateMachine
{
    List<GameObject> _playerInRange = new List<GameObject>();

    public override BaseState GetDefaultState() => new InteractState.Idle(this);

    public bool IsPlayerInRange(GameObject gameObject)
    {
        return this._playerInRange.Contains(gameObject);
    }

    public void DoInteract(GameObject gameObject)
    {
        if (this.IsPlayerInRange(gameObject) && this._currentState is InteractState.Idle)
        {
            InteractState.Obtaining obtainState = new InteractState.Obtaining(this, gameObject);
            this.ChangeState(obtainState);
        }
    }

    public void NotifyObjectEnter(GameObject player)
    {
        this._playerInRange.Add(player);
        Debug.Log("Object enter trigger: " + player);
    }
    
    public void NotifyObjectExit(GameObject player)
    {
        this._playerInRange.Remove(player);
        Debug.Log("Object exit trigger: " + player);
    }
}
