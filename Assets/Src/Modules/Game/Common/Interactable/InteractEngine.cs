using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class InteractEngine : StateMachine
    {
        List<GameObject> _playerInRange = new List<GameObject>();
        
        Model _model;
        public Model Model => this._model;

        protected override void Start()
        {
            base.Start();
            this._model = this.GetComponent<Stats>().Model;
        }

        public bool IsPlayerInRange(GameObject gameObject)
        {
            return this._playerInRange.Contains(gameObject);
        }

        public void DoInteract(GameObject gameObject)
        {
            if (this.IsPlayerInRange(gameObject) && this._currentState is State.Idle)
            {
                State.Interacting obtainState = new State.Interacting(this, gameObject);
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

        public override BaseState GetDefaultState() => new State.Idle(this);
    }
}