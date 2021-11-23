using System.Collections;
using System.Collections.Generic;
using Interactable.State;
using UnityEngine;

namespace Interactable
{
    public class InteractEngine : StateMachine
    {
        List<GameObject> _playerInRange = new List<GameObject>();

        GameObject _interactPlayer;
        public GameObject InteractPlayer => this._interactPlayer;

        protected InteractModel _model;
        public InteractModel Model => this._model;

        protected BaseState NextStateInteract => new Interacting(this);
        protected bool IsIdle => this._currentState is Idle;

        protected override void Start()
        {
            base.Start();
            this._model = this.GetComponent<InteractableStats>().Model;
        }

        public bool IsPlayerInRange(GameObject gameObject)
        {
            return this._playerInRange.Contains(gameObject);
        }

        public void DoInteract(GameObject gameObject)
        {
            if (this.IsPlayerInRange(gameObject) && this.IsIdle)
            {
                this._interactPlayer = gameObject;
                Interacting obtainState = this.NextStateInteract as Interacting;
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

        public override BaseState GetDefaultState() => new Idle(this);
    }
}