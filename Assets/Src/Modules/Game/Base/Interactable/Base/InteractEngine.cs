using System.Collections;
using System.Collections.Generic;
using Interactable.State;
using UnityEngine;

namespace Interactable
{
    public class InteractEngine : StateMachine
    {
        List<GameObject> _playerInRange = new List<GameObject>();

        protected GameObject _interactPlayer;
        public GameObject InteractPlayer => this._interactPlayer;

        protected InteractModel _model;
        public InteractModel Model => this._model;

        protected bool IsIdle => this._currentState is Idle;

        protected override void Start()
        {
            this._interactPlayer = null;
            base.Start();
            this.GetModel();
        }

        protected virtual void GetModel()
        {
            this._model = this.GetComponent<InteractableStats>().Model;
        }

        public bool IsPlayerInRange(GameObject gameObject)
        {
            float distance = Vector3.Distance(gameObject.transform.position, this.transform.position);
            return distance <= this.Model.InteractRadius;
        }

        public virtual void DoInteract(GameObject gameObject)
        {
            if (this.CanInteract(gameObject))
            {
                this._interactPlayer = gameObject;
                Interacting obtainState = new Interacting(this);
                this.ChangeState(obtainState);
            }
        }

        public override BaseState GetDefaultState() => new Idle(this);

        public virtual void OnInteractSuccesful()
        {
            //TODO: Override to do effect when interact object at full time
            this.gameObject.SetActive(false);
        }

        public virtual void OnInteractFailed()
        {
            //TODO: Override to do effect when not nteract object at full time 
        }

        protected virtual bool CanInteract(GameObject gameObject)
        {
            return this.InteractPlayer == null && this.IsPlayerInRange(gameObject) && this.IsIdle;
        }

        public void ResetInteractPlayer()
        {
            this._interactPlayer = null;
        }
    }
}