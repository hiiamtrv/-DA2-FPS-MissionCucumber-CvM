using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable;
using Character;

namespace ShieldCenter
{
    public class ShieldCenterEngine : InteractEngine
    {
        ShieldCenterModel _model;
        public ShieldCenterModel Model => _model;

        float _lastInteractTime;

        protected override void GetModel()
        {
            this._model = this.GetComponent<ShieldCenterStats>().Model;
        }

        protected override bool CanInteract(GameObject gameObject)
        {
            CharacterStats stats = gameObject.GetComponent<CharacterStats>();
            return base.CanInteract(gameObject) && stats != null && stats.CharacterSide == CharacterSide.MICE;
        }

        public void DoInteract(GameObject gameObject)
        {
            if (CanInteract(gameObject))
            {
                this._lastInteractTime = this.Model.GetLastedCheckPoint(this._lastInteractTime);
                this._interactPlayer = gameObject;
                SCInteracting obtainState = new SCInteracting(this, this._lastInteractTime);
                this.ChangeState(obtainState);
            }
        }

        protected override void Update()
        {
            base.Update();
            if (this._currentState is SCInteracting)
            {
                this._lastInteractTime = ((SCInteracting)this._currentState).InteractTime;
            }
        }

        public virtual void OnInteractSuccesful()
        {
            EventCenter.Publish(EventId.SHIELD_CENTER_DESTROYED);
            this.gameObject.SetActive(false);
        }
    }
}
