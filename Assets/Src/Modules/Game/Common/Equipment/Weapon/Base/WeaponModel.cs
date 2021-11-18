using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponModel
    {
        float _baseFireRate;
        float _baseDamage;
        float _baseShotSpread;
        float _baseReloadSpeed;

        float _fireRate;
        float _damage;
        float _shotSpread;
        float _reloadSpeed;
        int _magazineSize;
        int _maxAmmo;

        public WeaponModel(float fireRate, float damage, float shotSpread, float reloadSpeed, int magazineSize, int maxAmmo)
        {
            this._baseFireRate = fireRate;
            this._baseDamage = damage;
            this._baseShotSpread = shotSpread;
            this._baseReloadSpeed = reloadSpeed;

            this._magazineSize = magazineSize;
            this._maxAmmo = maxAmmo;

            this._fireRate = this._baseFireRate;
            this._damage = this._baseDamage;
            this._shotSpread = this._baseShotSpread;
            this._reloadSpeed = this._baseReloadSpeed;
        }

        public float FireRate { get => this._fireRate; set => this._fireRate = value; }
        public float Damage { get => this._damage; set => this._damage = value; }
        public float ShotSpread { get => this._shotSpread; set => this._shotSpread = value; }
        public float ReloadSpeed { get => this._reloadSpeed; set => this._reloadSpeed = value; }
        public int MagazineSize => this._magazineSize;
        public int MaxAmmo => this._maxAmmo;

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

        public float ReloadSpeedPercent
        {
            get => this._reloadSpeed / this._baseReloadSpeed;
            set => this._reloadSpeed = this._baseReloadSpeed * value;
        }
    }
}
