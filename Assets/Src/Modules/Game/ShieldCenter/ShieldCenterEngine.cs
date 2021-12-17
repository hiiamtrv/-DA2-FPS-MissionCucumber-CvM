using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable;
using Character;

namespace ShieldCenter
{
    public class ShieldCenterEngine : InteractEngine
    {
        [SerializeField] GameObject[] appearInEnabled;
        [SerializeField] GameObject[] appearInDisabled;

        static ShieldCenterEngine _ins;
        public static ShieldCenterEngine Ins => _ins;

        public new ShieldCenterModel Model => (ShieldCenterModel)this._model;

        float _lastInteractTime;

        void Awake()
        {
            _ins = this;
            foreach (GameObject go in appearInDisabled) go.SetActive(false);
        }

        protected override void GetModel()
        {
            this._model = this.GetComponent<ShieldCenterStats>().Model;
        }

        protected override bool CanInteract(GameObject gameObject)
        {
            CharacterStats stats = gameObject.GetComponent<CharacterStats>();
            return stats != null && stats.CharacterSide == CharacterSide.MICE && base.CanInteract(gameObject);
        }

        public override void DoInteract(GameObject gameObject)
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

        public override void OnInteractSuccesful()
        {
            EventCenter.Publish(EventId.SHIELD_CENTER_DESTROYED);
            this.enabled = false;

            foreach (GameObject go in appearInEnabled) go.SetActive(false);
            foreach (GameObject go in appearInDisabled) go.SetActive(true);
        }
    }
}
