using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Projectile
{
    public class ProjectileEngine : StateMachine
    {
        [SerializeField] AudioClip _soundShoot;
        [SerializeField] AudioClip _soundHit;

        ProjectileStats _model;
        public ProjectileStats Model => this._model;
        public int NumBounce { get => Model.NumBounce; set => Model.NumBounce = value; }
        public float FlySpeed => Model.FlySpeed;

        Vector3 _flyVector;
        public Vector3 FlyVector { get => _flyVector; set => _flyVector = value; }

        GameObject _owner;
        public GameObject Owner { set => _owner = value; }

        GameObject _collidedObject;
        public GameObject CollidedObject => _collidedObject;

        Action _onHit;
        public Action OnHit { get => _onHit; set => _onHit = value; }

        public override BaseState GetDefaultState() => new State.FlyState(this);

        protected override void Start()
        {
            this._model = this.GetComponent<ProjectileStats>();
            if (this._model == null)
            {
                UnityEngine.Debug.LogWarning("[{0}] Model is not set, destroy self");
                Destroy(this.gameObject);
            }
            else
            {
                base.Start();
                this._flyVector = Vector3.forward;
                this.gameObject.PlaySound(_soundShoot);
            }

            LeanTween.delayedCall(this.Model.TimeDestroy, () =>
            {
                if (this != null) Destroy(this.gameObject);
            });
        }

        public void HitObject()
        {
            this.gameObject.PlaySound(_soundHit);
            if (_onHit != null) _onHit();
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

        void OnTriggerEnter(Collider collider)
        {
            if (this != null && collider.gameObject != _owner)
            {
                this._collidedObject = collider.gameObject;
                (this._currentState as State.IProjectileState).OnCollsion();
            }
        }
    }
}