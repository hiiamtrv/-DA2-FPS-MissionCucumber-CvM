using System.Collections;
using System.Collections.Generic;
using AI;
using Interactable.State;
using UnityEngine;

namespace Interactable
{
    public class InteractEngine : StateMachine, IInteractable
    {
        List<GameObject> _playerInRange = new List<GameObject>();

        protected GameObject _interactPlayer;
        public GameObject InteractPlayer => this._interactPlayer;

        protected InteractModel _model;
        public InteractModel Model => this._model;

        protected virtual BaseState InteractingState => new Interacting(this);

        protected bool IsIdle => this._currentState is Idle;

        protected bool _isPlayerAi = false;
        public bool IsPlayerAi => _isPlayerAi;
        protected float _aiInteractStartTime;
        protected float _aiInteractCount;

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

        protected override void Update()
        {
            if (this._isPlayerAi) Debug.Log("Check who is interact", this._interactPlayer);
            base.Update();
            // if (this._isPlayerAi && this._interactPlayer != null)
            // {
            //     var offset = Mathf.Abs((Time.time - this._aiInteractStartTime) - this._aiInteractCount);
            //     if (offset > Time.deltaTime * 2)
            //     {
            //         this._interactPlayer.GetComponent<AIBaseEngine>().AbortInteract();
            //     }
            // }
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
            Debug.Log("Can interract ?",
                this.InteractPlayer == null,
                this.IsPlayerInRange(gameObject),
                this.IsIdle,
                this.IsPlayerLooking(gameObject)
            );

            return this.InteractPlayer == null && this.IsPlayerInRange(gameObject)
                && this.IsIdle && this.IsPlayerLooking(gameObject);
        }

        public void ResetInteractPlayer()
        {
            this._interactPlayer = null;
        }

        public virtual bool TriggerInteract(GameObject ai, bool isAi = false)
        {
            if (CanInteract(ai) && isAi)
            {
                Debug.Log("Trigger interact", ai);
                this.DoInteract(ai);
                this._isPlayerAi = true;
                this._aiInteractStartTime = Time.time;
                this._aiInteractCount = Time.deltaTime;

                return true;
            }
            else return false;
        }

        public virtual bool KeepInteract(GameObject ai)
        {
            if (this.InteractPlayer == ai && this.IsPlayerInRange(gameObject))
            {
                this._aiInteractCount += Time.deltaTime;
                Debug.Log("Player keep interact success");
                return true;
            }
            else
            {
                Debug.Log("Player keep interact failed"); 
                return false;
            }
        }

        public virtual void AbortInteract(GameObject ai)
        {
            if (this.InteractPlayer == ai)
            {
                // Debug.Log("Abort interact", ((Interacting)this._currentState).InteractTime);

                this._isPlayerAi = false;
                this._aiInteractStartTime = 0;
                this._aiInteractCount = 0;
                this.ChangeState(this.GetDefaultState());
            }
        }

        public virtual GameObject GetGameObject()
        {
            return this.gameObject;
        }
    }
}