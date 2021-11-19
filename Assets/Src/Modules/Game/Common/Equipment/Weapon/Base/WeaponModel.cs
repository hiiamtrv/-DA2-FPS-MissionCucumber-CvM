using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Equipments;

namespace Weapons
{
    public class WeaponModel : EquipmentModel
    {
        float _baseFireRate;
        float _baseDamage;
        float _baseShotSpread;
        float _baseReloadTime;

        float _fireRate;
        float _damage;
        float _shotSpread;
        float _reloadTime;
        int _magazineSize;
        int _totalAmmo;
        int _remainAmmo;

        public WeaponModel(float equipTime, float fireRate, float damage, float shotSpread
                            , float reloadTime, int magazineSize, int totalAmmo) : base(equipTime)
        {
            this._baseFireRate = fireRate;
            this._baseDamage = damage;
            this._baseShotSpread = shotSpread;
            this._baseReloadTime = reloadTime;

            this._magazineSize = magazineSize;
            this._totalAmmo = totalAmmo;

            this._fireRate = this._baseFireRate;
            this._damage = this._baseDamage;
            this._shotSpread = this._baseShotSpread;
            this._reloadTime = this._baseReloadTime;
            this._remainAmmo = this._magazineSize;
        }

        public float FireRate { get => this._fireRate; set => this._fireRate = value; }
        public float Damage { get => this._damage; set => this._damage = value; }
        public float ShotSpread { get => this._shotSpread; set => this._shotSpread = value; }
        public float ReloadTime { get => this._reloadTime; set => this._reloadTime = value; }
        public int RemainAmmo { get => this._remainAmmo; set => this._remainAmmo = value; }
        public int MagazineSize => this._magazineSize;
        public int TotalAmmo { get => this._totalAmmo; set => this._totalAmmo = value; }

        public float FireRatePercent
        {
            get => this._fireRate / this._baseDamage;
            set => this._fireRate = this._baseFireRate * value;
        }

        public float DamagePercent
        {
            get => this._damage / this._baseDamage;
            set => this._damage = this._baseDamage * value;
        }

        public float ShotSpreadPercent
        {
            get => this._shotSpread / this._baseShotSpread;
            set => this._shotSpread = this._baseShotSpread * value;
        }

        public float ReloadTimePercent
        {
            get => this._reloadTime / this._baseReloadTime;
            set => this._reloadTime = this._baseReloadTime * value;
        }
    }
}
