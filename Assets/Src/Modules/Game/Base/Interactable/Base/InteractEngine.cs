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

        protected virtual BaseState InteractingState => new Interacting(this);

        protected bool IsIdle => this._currentState is Idle;

        protected override void Start()
        {
            this._interactPlayer = null;
            base.Start();
            this.GetModel();

            EventCenter.Subcribe(EventId.INTERACT_REQUEST, (pubData) =>
            {
                PubData.InteractRequest data = pubData as PubData.InteractRequest;
                if (data.InteractObject == this.gameObject)
                {
                    this.DoInteract(data.Dispatcher);
                }
            });
        }

        protected virtual void GetModel()
        {
            this._model = this.GetComponent<InteractableStats>().Model;
        }

        public virtual bool IsPlayerInRange(GameObject gameObject)
        {
            float distance = Vector3.Distance(gameObject.transform.position, this.transform.position);
            return distance <= this.Model.InteractRadius;
        }

        public virtual bool IsPlayerLooking(GameObject gameObject)
        {
            const string EYE_POINT = "EyePoint";
            Transform eyePoint = gameObject.transform.Find(EYE_POINT);
            if (eyePoint == null) return false;

            Vector3 posChar = gameObject.transform.position;
            Vector3 objPos = this.transform.position;
            float angle = Vector3.Angle((objPos - posChar), eyePoint.forward);

            const float MAX_INTERACTABLE_ANGLE = 30f;
            return (angle <= MAX_INTERACTABLE_ANGLE);
        }

        public virtual void DoInteract(GameObject gameObject)
        {
            if (this.CanInteract(gameObject))
            {
                this._interactPlayer = gameObject;
                BaseState obtainState = this.InteractingState;
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
            return this.InteractPlayer == null && this.IsPlayerInRange(gameObject)
                && this.IsIdle && this.IsPlayerLooking(gameObject);
        }

        public void ResetInteractPlayer()
        {
            this._interactPlayer = null;
        }
    }
}