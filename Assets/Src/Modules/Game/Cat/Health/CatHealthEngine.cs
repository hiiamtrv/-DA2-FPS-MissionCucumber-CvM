using System.Collections;
using System.Collections.Generic;
using Cats.HealthState;
using Character;
using UnityEngine;

namespace Cats
{
    public class CatHealthEngine : HealthEngine
    {
        [SerializeField] GameObject _catNormalModel;
        [SerializeField] GameObject _catRageModel;
        bool _isDying = false;
        public bool IsDying => _isDying;

        public override BaseState GetDefaultState() => new CatNormal(this);
        protected virtual BaseState CatWeakened => new CatWeakened(this);

        protected override void Start()
        {
            EventCenter.Subcribe(EventId.SHIELD_CENTER_DESTROYED, (object pubData) =>
            {
                UnityEngine.Debug.Log("Change Cat State to weakened !");
                this.ChangeState(this.CatWeakened);
            });

            EventCenter.Subcribe(EventId.CAT_DYING, (object pubData) =>
            {
                GameObject cat = pubData as GameObject;
                if (cat == this.gameObject) this._isDying = true;
            });

            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            this._catNormalModel.SetActive(this._isDying == false);
            this._catRageModel.SetActive(this._isDying == true);
        }
    }
}
