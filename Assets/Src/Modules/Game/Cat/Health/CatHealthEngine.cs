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

        [SerializeField] GameObject _catDownSoundObject;

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

            EventCenter.Subcribe(EventId.CAT_DOWN, (e) =>
            {
                PubData.CatDown data = e as PubData.CatDown;
                if (data.Dispatcher == this.gameObject)
                {
                    GameObject catDownSoundObj = Instantiate(_catDownSoundObject, Vector3.zero, Quaternion.identity, transform);
                    LeanTween.delayedCall(data.DownTime, () =>
                    {
                        Destroy(catDownSoundObj);
                    });
                }
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
