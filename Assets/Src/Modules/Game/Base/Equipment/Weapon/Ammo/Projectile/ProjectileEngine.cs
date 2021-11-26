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
        public bool ApplyGravity => Model.ApplyGravity;
        public float FlySpeed => Model.FlySpeed;

        Vector3 _flyVector;
        public Vector3 FlyVector { get => _flyVector; set => _flyVector = value; }

        GameObject _collidedObject;
        public GameObject CollidedObject => _collidedObject;

        Action<object> _onExplode;
        public Action<object> OnExplode { get => _onExplode; set => _onExplode = value; }

        public override BaseState GetDefaultState() => new State.FlyState(this);

        protected override void Start()
        {
            this._model = this.GetComponent<ProjectileStats>();
            if (this._model == null)
            {
                Debug.LogWarning("[{0}] Model is not set, destroy self");
                Destroy(this.gameObject);
            }
            else
            {
                base.Start();
                this._flyVector = Vector3.forward;
            }
        }

        public void Explode()
        {
            if (_onExplode != null) _onExplode(this._collidedObject);
        }

        void OnCollisionEnter(Collision collisionInfo)
        {
            this._collidedObject = collisionInfo.gameObject;
            (this._currentState as State.IProjectileState).OnCollsion();
        }
    }
}