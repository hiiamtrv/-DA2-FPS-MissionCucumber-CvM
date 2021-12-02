using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class ProjectileEngine : StateMachine
    {
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
            }

            LeanTween.delayedCall(this.Model.TimeDestroy, () =>
            {
                if (this) Destroy(this.gameObject);
            });
        }

        public void HitObject()
        {
            if (_onHit != null) _onHit();
            Destroy(this.gameObject);
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject != _owner)
            {
                this._collidedObject = collider.gameObject;
                (this._currentState as State.IProjectileState).OnCollsion();
            }
        }
    }
}