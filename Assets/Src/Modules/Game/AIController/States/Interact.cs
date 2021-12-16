using System.Collections;
using System.Collections.Generic;
using Character;
using Interactable;
using UnityEngine;

namespace AI
{
    public class Interact : BaseState, IAIState
    {
        const float SAFE_INTERACT = 0.5f;

        public bool IsTargetLockMode() { return true; }

        public AIBaseEngine StateMachine => (AIBaseEngine)this._stateMachine;
        public Interact(StateMachine stateMachine) : base(stateMachine) { }

        bool _isReachTarget = false;
        GameObject _interactable;

        float rotateSpeed;

        public override void OnEnter()
        {
            base.OnEnter();
            _interactable = this.StateMachine.InteractingObject.GetGameObject();
            Vector3 destination = _interactable.transform.position;
            this.StateMachine.agent.SetDestination(destination);

            EventCenter.Subcribe(EventId.INTERACT_END, (pubData) =>
            {
                PubData.InteractEnd data = pubData as PubData.InteractEnd;
                if (data.Dispatcher == this._gameObject)
                {
                    this.CancelInteractState();
                }
            });
        }

        public override void LogicUpdate()
        {
            this._gameObject.GetComponent<Eye>().LookAt(_interactable);
            if (this._isReachTarget)
            {
                bool interactSuccess = this.StateMachine.InteractingObject.KeepInteract(this._gameObject);
                if (!interactSuccess) this.CancelInteractState();
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            if (!this._isReachTarget)
            {
                float radius = _interactable.GetComponent<InteractEngine>().Model.InteractRadius;
                float distance = Vector3.Distance(this._gameObject.transform.position, this._interactable.transform.position);

                Debug.Log("Check wait interact", distance, radius);

                if (distance <= radius * SAFE_INTERACT)
                {
                    this._gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                    bool interactSuccess = this.StateMachine.InteractingObject.TriggerInteract(this._gameObject, true);
                    this._isReachTarget = true;

                    if (!interactSuccess) this.CancelInteractState();
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            float speed = this._gameObject.GetComponent<CharacterStats>().Speed;
            this._gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            this.StateMachine.InteractingObject.AbortInteract(this._gameObject);
        }

        void CancelInteractState()
        {
            BaseState nextState = this.StateMachine.RollNextState();
            this.SetNextState(nextState);
        }
    }
}
