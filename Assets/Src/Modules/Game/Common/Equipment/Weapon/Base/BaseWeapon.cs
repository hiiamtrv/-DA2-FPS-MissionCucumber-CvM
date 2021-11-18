using System.Collections;
using System.Collections.Generic;
using Character;
using Equipments;
using UnityEngine;

namespace Weapons
{
    public class BaseWeapon : Equipment
    {
        [SerializeField] WeaponName _name;
        public WeaponName Name => this._name;

        protected WeaponModel _model;
        public WeaponModel Model => this._model;

        protected int _isFiring;
        protected int _magazineAmmo;
        protected int _maxAmmo;
        protected GunState _gunState;

        [SerializeField] Camera _eye;
        [SerializeField] AudioClip _soundGunShot;
        [SerializeField] AudioClip _soundNoAmmo;
        [SerializeField] AudioClip _soundEquip;
        AudioSource _audio = new AudioSource();

        protected override void Start()
        {
            this._model = this.GetComponent<WeaponStats>().Model;
            this._maxAmmo = this.Model.MaxAmmo;
            this._magazineAmmo = this.Model.MagazineSize;
            base.Start();
        }

        void Update()
        {
            if (this.IsReady && InputMgr.Shoot)
            {
                this.Shoot();
            }
        }

        protected void Shoot()
        {
            // this._audio.PlayOneShot(_soundEquip);
            var targets = this.Target;
            if (targets != null)
            {
                foreach (GameObject target in targets)
                {
                    this.DoHitEffect(target);
                }
            }
        }

        public override void OnEquiped()
        {
            // this._audio.PlayOneShot(this._soundEquip);
            base.OnEquiped();
        }

        public override void OnUnequiped()
        {
            base.OnUnequiped();
        }

        protected virtual void DoHitEffect(GameObject target)
        {
            //TODO: override to inflict effects other than damage
            HealthEngine health = target.GetComponent<HealthEngine>();
            if (health)
            {
                health.InflictDamage(this.Model.Damage);
            }
        }

        public virtual GameObject[] Target
        {
            get
            {
                float screenX = Screen.width * 0.5f;
                float screenY = Screen.height * 0.5f;
                RaycastHit hit;
                Ray ray = this._eye.ScreenPointToRay(new Vector2(screenX, screenY));

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject[] targets = { hit.collider.gameObject };
                    return targets;
                }
                else return null;
            }
        }
    }

    public enum GunState
    {
        READY_TO_FIRE,
        RECOIL,
        RELOADING,
    }
}
