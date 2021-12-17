using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectile;

namespace FireBall
{
    public class ProjectileFireball : ProjectileEngine
    {
        [SerializeField] GameObject _explosion;

        public override void HitObject()
        {
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            base.HitObject();
        }
    }
}
