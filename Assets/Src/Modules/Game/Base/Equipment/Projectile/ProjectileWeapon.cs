using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Projectile
{
    public class ProjectileWeapon : AmmoWeapon
    {
        [SerializeField] GameObject _projectile;

        const string MUZZLE = "Muzzle";

        GameObject _muzzle;
        public GameObject Muzzle => _muzzle;

        protected override void Awake()
        {
            _muzzle = this.transform.Find(MUZZLE).gameObject;
            base.Awake();
        }

        protected override void Shoot()
        {
            //create projectile
            Vector3 muzzlePoint = _muzzle.transform.position;
            Vector3 aimPoint = this.GetRayEndpoint();

            GameObject newProjectile = Instantiate(_projectile, muzzlePoint, Quaternion.identity);
            newProjectile.transform.LookAt(aimPoint);
            ProjectileEngine engine = newProjectile.GetComponent<ProjectileEngine>();
            engine.OnHit = () => base.DoHitEffect(engine.CollidedObject);
            engine.Owner = _owner;

            base.Shoot();
        }

        protected override void DoHitEffect(GameObject target)
        {
            //do nothing
        }

        Vector3 GetRayEndpoint()
        {
            RaycastHit hit;
            Ray ray = this._eye.ScreenPointToRay(this.AIM_POINT);
            float weaponRange = this.Model.ShotRange;

            if (Physics.Raycast(ray, out hit, weaponRange))
            {
                return hit.point;
            }
            else
            {
                return this.transform.position + ray.direction * weaponRange;
            }
        }
    }
}