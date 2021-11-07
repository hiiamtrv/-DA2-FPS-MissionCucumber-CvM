using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class InteractEngine : StateMachine
    {
        List<GameObject> _playerInRange = new List<GameObject>();

        public override BaseState GetDefaultState() => new State.Idle(this);

        public bool IsPlayerInRange(GameObject gameObject)
        {
            return this._playerInRange.Contains(gameObject);
        }

        public void DoInteract(GameObject gameObject)
        {
            if (this.IsPlayerInRange(gameObject) && this._currentState is State.Idle)
            {
                State.Obtaining obtainState = new State.Obtaining(this, gameObject);
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
}