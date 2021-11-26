using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class ProjectileStats : MonoBehaviour
    {
        [SerializeField] int _numBounce;
        public int NumBounce { get => _numBounce; set => _numBounce = value; }

        [SerializeField] float _flySpeed;
        public float FlySpeed => this._flySpeed;

        [SerializeField] int _timeDestroy;
        public float TimeDestroy => _timeDestroy;
    }
}