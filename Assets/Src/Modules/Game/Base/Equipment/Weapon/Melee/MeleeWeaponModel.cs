using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Equipments;

namespace Weapons
{
    public class MeleeWeaponModel : EquipmentModel
    {
        float _baseAttackSpeed;
        float _baseDamage;
        float _baseAttackRange;

        float _attackSpeed;
        float _damage;
        float _attackRange;

        public MeleeWeaponModel(float equipTime, float attackSpeed, float damage, float attackRange) : base(equipTime)
        {
            this._baseAttackSpeed = attackSpeed;
            this._baseDamage = damage;
            this._baseAttackRange = attackRange;

            this._attackSpeed = this._baseAttackSpeed;
            this._damage = this._baseDamage;
            this._attackRange = this._baseAttackRange;
        }

        public float AttackSpeed { get => this._attackSpeed; set => this._attackSpeed = value; }
        public float Damage { get => this._damage; set => this._damage = value; }
        public float AttackRange { get => this._attackRange; set => this._attackRange = value; }

        public float AttackSpeedPercent
        {
            get => this._attackSpeed / this._baseAttackSpeed;
            set => this._attackSpeed = this._baseAttackSpeed * value;
        }

        public float DamagePercent
        {
            get => this._damage / this._baseDamage;
            set => this._damage = this._baseDamage * value;
        }

        public float AttackRangePercent
        {
            get => this._attackRange / this._baseAttackRange;
            set => this._attackRange = this._baseAttackRange * value;
        }
    }
}
