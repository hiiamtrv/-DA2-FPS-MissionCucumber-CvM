using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponModel
    {
        float _baseFireRate;
        float _baseEquipSpeed;
        float _baseShotSpread;
        float _baseReloadSpeed;

        float _fireRate;
        float _equipSpeed;
        float _shotSpread;
        float _reloadSpeed;
        int _magazineSize;

        public WeaponModel(float fireRate, float equipSpeed, float shotSpread, float reloadSpeed, int magazineSize)
        {
            this._baseFireRate = fireRate;
            this._baseEquipSpeed = equipSpeed;
            this._baseShotSpread = shotSpread;
            this._baseReloadSpeed = reloadSpeed;

            this._magazineSize = magazineSize;

            this._fireRate = this._baseFireRate;
            this._equipSpeed = this._baseEquipSpeed;
            this._shotSpread = this._baseShotSpread;
            this._reloadSpeed = this._baseReloadSpeed;
        }

        public float FireRate { get => this._fireRate; set => this._fireRate = value; }
        public float EquipSpeed { get => this._equipSpeed; set => this._equipSpeed = value; }
        public float ShotSpread { get => this._shotSpread; set => this._shotSpread = value; }
        public float ReloadSpeed { get => this._reloadSpeed; set => this._reloadSpeed = value; }
        public float MagazineSize => this._magazineSize;

        public float FireRatePercent
        {
            get => this._fireRate / this._baseEquipSpeed; 
            set => this._fireRate = this._baseFireRate * value;
        }

        public float EquipSpeedPercent
        {
            get => this._equipSpeed / this._baseEquipSpeed; 
            set => this._equipSpeed = this._baseEquipSpeed * value;
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
