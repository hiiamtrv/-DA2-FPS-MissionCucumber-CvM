using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Projectile
{
    public class ProjectileWeapon : AmmoWeapon
    {
        [SerializeField] GameObject _projectile;

        protected override void Shoot()
        {
            Camera eye = this._eye;
            GameObject newProjectile = Instantiate(_projectile, eye.transform.position, eye.transform.rotation);
            newProjectile.transform.LookAt(eye.transform.position);

            base.Shoot();
        }
    }
}