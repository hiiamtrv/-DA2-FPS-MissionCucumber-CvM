using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Projectile;
using UnityEngine;
using Weapons;

namespace LaserGun
{
    public class LaserGunEngine : AmmoWeapon
    {
        [SerializeField] GameObject _projectile;

        const string MUZZLE = "Muzzle";

        GameObject _muzzle;
        public GameObject Muzzle => _muzzle;

        public LaserGunModel Model => this._model as LaserGunModel;

        protected override void Awake()
        {
            base.Awake();
            _muzzle = this.transform.Find(MUZZLE).gameObject;
        }

        protected override void GetModel()
        {
            this._model = this.GetComponent<LaserGunStats>().Model;
        }

        protected override void Shoot()
        {
            this.RunAnimShoot();

            LeanTween.delayedCall(Time.deltaTime * 2, () =>
            {
                //create projectile
                Vector3 muzzlePoint = _muzzle.transform.position;
                Vector3 aimPoint = this.GetRayEndpoint();

                GameObject newProjectile = PhotonNetwork.Instantiate(_projectile.name, muzzlePoint, Quaternion.identity);
                // GameObject newProjectile = Instantiate(_projectile, muzzlePoint, Quaternion.identity);
                newProjectile.transform.LookAt(aimPoint);
                ProjectileEngine engine = newProjectile.GetComponent<ProjectileEngine>();
                engine.Owner = _owner;

                base.Shoot();
            });
        }

        protected override void DoHitEffect(GameObject target)
        {
            Vector3 muzzlePoint = _muzzle.transform.position;
            Vector3 targetPoint = target.transform.position;

            float flySpeed = _projectile.GetComponent<ProjectileStats>().FlySpeed;
            float distance = Vector3.Distance(muzzlePoint, targetPoint);
            float timeDelayDamage = distance / flySpeed;
            LeanTween.delayedCall(timeDelayDamage, () => base.DoHitEffect(target));
        }

        Vector3 GetRayEndpoint()
        {
            RaycastHit hit;
            // Ray ray = this._eye.ScreenPointToRay(this.AIM_POINT);
            Ray ray = new Ray(this._eye.transform.position, this._eye.transform.forward);
            float weaponRange = this.Model.ShotRange;

            if (Physics.Raycast(ray, out hit, weaponRange))
            {
                return hit.point;
            }
            else
            {
                return this._eye.transform.position + ray.direction * weaponRange;
            }
        }

        void RunAnimShoot(float time = 0.5f)
        {
            var sequence = LeanTween.sequence();
            sequence.insert(
                LeanTween.rotateX(this.gameObject, -10, time / 2)
                    .setEaseInCubic()
            );
            sequence.insert(
                       LeanTween.rotateX(this.gameObject, 0, time / 2)
            );
        }
    }
}
