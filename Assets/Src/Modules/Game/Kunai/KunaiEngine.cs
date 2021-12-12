using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Projectile;
using Photon.Pun;

namespace Kunai
{
    public class KunaiEngine : AmmoWeapon
    {
        [SerializeField] GameObject _projectile;

        const string MUZZLE = "Muzzle";

        GameObject _muzzle;
        public GameObject Muzzle => _muzzle;

        public KunaiModel Model => this._model as KunaiModel;

        protected override void Awake()
        {
            base.Awake();
            _muzzle = this.transform.Find(MUZZLE).gameObject;
        }

        protected override void GetModel()
        {
            this._model = this.GetComponent<KunaiStats>().Model;
        }

        protected override void Shoot()
        {
            //create projectile
            Vector3 muzzlePoint = _muzzle.transform.position;
            Vector3 aimPoint = this.GetRayEndpoint();

            GameObject newProjectile = PhotonNetwork.Instantiate(_projectile.name, muzzlePoint, Quaternion.identity);
            // GameObject newProjectile = Instantiate(_projectile, muzzlePoint, Quaternion.identity);
            newProjectile.transform.LookAt(aimPoint);
            ProjectileEngine engine = newProjectile.GetComponent<ProjectileEngine>();
            engine.OnHit = () => base.DoHitEffect(engine.CollidedObject);
            engine.Owner = _owner;

            this._equipmentObject.gameObject.SetActive(false);
            LeanTween.delayedCall(1 / this.Model.FireRate, () =>
            {
                this._equipmentObject.gameObject.SetActive(true);
            });

            base.Shoot();
        }

        protected override void DoHitEffect(GameObject target)
        {
            //do nothing
        }

        Vector3 GetRayEndpoint()
        {
            RaycastHit hit;
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
    }
}