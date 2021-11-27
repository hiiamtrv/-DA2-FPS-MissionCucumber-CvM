using System.Collections;
using System.Collections.Generic;
using Character;
using Equipments;
using UnityEngine;

namespace Weapons
{
    public class AmmoWeapon : Equipment
    {
        protected readonly Vector2 AIM_POINT = new Vector2(
            Screen.width * 0.5f,
            Screen.height * 0.5f
        );

        protected AmmoWeaponModel _model;
        public AmmoWeaponModel Model => this._model;

        protected int _isFiring;

        protected GunState _gunState;

        [SerializeField] protected Camera _eye;
        [SerializeField] protected AudioClip _soundGunShot;
        [SerializeField] protected AudioClip _soundNoAmmo;
        [SerializeField] protected AudioClip _soundEquip;
        AudioSource _audio = new AudioSource();

        protected override void Start()
        {
            this._model = this.GetComponent<AmmoWeaponStats>().Model;
            this.Model.TotalAmmo -= this.Model.MagazineSize;
            base.Start();
        }

        protected virtual void Update()
        {
            if (this.IsReady && this.CanShoot && InputMgr.StartShoot)
            {
                this.Shoot();
            }
        }

        protected virtual void Shoot()
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
            this.RecalulateAmmo();
        }

        public override void OnEquiped()
        {
            // this._audio.PlayOneShot(this._soundEquip);
            base.OnEquiped();
            LeanTween.delayedCall(this._equipTime / 2, () =>
            {
                if (this != null)
                    EventCenter.Publish(
                        EventId.WEAPON_AMMO_EQUIP,
                        new PubData.WeaponAmmoEquip(this.Owner, this.Model)
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
                RaycastHit hit;
                Ray ray = this._eye.ScreenPointToRay(this.AIM_POINT);
                float weaponRange = this.Model.ShotRange;

                if (Physics.Raycast(ray, out hit, weaponRange))
                {
                    targets.Add(hit.collider.gameObject);
                }

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

        protected void RecalulateAmmo()
        {
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
    }

    public enum GunState
    {
        READY_TO_FIRE,
        RECOIL,
        RELOADING,
    }
}
