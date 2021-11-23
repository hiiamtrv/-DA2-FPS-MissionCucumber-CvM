using System.Collections;
using System.Collections.Generic;
using Character;
using Equipments;
using UnityEngine;

namespace Weapons
{
    public class BaseWeapon : Equipment
    {
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
            this._maxAmmo = this.Model.TotalAmmo;
            this._magazineAmmo = this.Model.MagazineSize;
            base.Start();
        }

        void Update()
        {
            if (this.IsReady && this.CanShoot)
            {
                this.Shoot();
            }
        }

        protected void Shoot()
        {
            this.Model.RemainAmmo--;

            // this._audio.PlayOneShot(_soundEquip);
            var targets = this.Target;
            if (targets.Length > 0)
            {
                foreach (GameObject target in targets)
                {
                    this.DoHitEffect(target);
                }
            }

            this.PublishAmmoChange();

            if (this.Model.RemainAmmo == 0)
            {
                this._gunState = GunState.RELOADING;
                if (this.Model.TotalAmmo > 0)
                {
                    LeanTween.delayedCall(
                        this.Model.ReloadTime,
                        () =>
                        {
                            int nextMagazine = System.Math.Min(this.Model.MagazineSize, this.Model.TotalAmmo);
                            this.Model.RemainAmmo = nextMagazine;
                            this.Model.TotalAmmo -= nextMagazine;
                            this._gunState = GunState.READY_TO_FIRE;
                            this.PublishAmmoChange();
                        }
                    );
                }
            }
            else
            {
                this._gunState = GunState.RECOIL;
                LeanTween.delayedCall(
                    1 / this.Model.FireRate,
                    () => this._gunState = GunState.READY_TO_FIRE
                );
            }
        }

        public override void OnEquiped()
        {
            // this._audio.PlayOneShot(this._soundEquip);
            base.OnEquiped();
            LeanTween.delayedCall(this._equipTime / 2, () =>
            {
                if (this != null)
                    EventCenter.Publish(
                        EventId.WEAPON_EQUIP,
                        new PubData.WeaponEquip(this.Owner, this.Model)
                    );
            });

        }

        public override void OnUnequiped()
        {
            base.OnUnequiped();
            EventCenter.Publish(
                EventId.WEAPON_UNEQUIP,
                new PubData.WeaponUnequip(this.Owner)
            );
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
                List<GameObject> targets = new List<GameObject>();

                float screenX = Screen.width * 0.5f;
                float screenY = Screen.height * 0.5f;
                RaycastHit hit;
                Ray ray = this._eye.ScreenPointToRay(new Vector2(screenX, screenY));
                float weaponRange = this.Model.ShotRange;

                if (Physics.Raycast(ray, out hit, weaponRange))
                {
                    targets.Add(hit.collider.gameObject);
                }
                targets.Add(this._owner);

                return targets.ToArray();
            }
        }

        protected virtual bool CanShoot => InputMgr.Shoot && this._gunState == GunState.READY_TO_FIRE;

        protected void PublishAmmoChange()
        {
            EventCenter.Publish(
                EventId.WEAPON_AMMO_CHANGE,
                new PubData.WeaponAmmoChange(this.Owner, this.Model.RemainAmmo, this.Model.TotalAmmo)
            );
        }
    }

    public enum GunState
    {
        READY_TO_FIRE,
        RECOIL,
        RELOADING,
    }
}
