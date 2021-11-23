using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Weapons;

namespace Equipments
{
    public class EquipmentMgr : MonoBehaviour
    {
        [SerializeField] GameObject _objectWeapon;
        [SerializeField] GameObject _objectUtility;

        [SerializeField] GameObject[] _reserveWeapon;
        [SerializeField] GameObject[] _reserveUtility;

        BaseWeapon _weapon;
        public BaseWeapon Weapons => this._weapon;

        BaseUtility _utility;
        public BaseUtility Utility => this._utility;

        void Awake()
        {
            this._weapon = this._objectWeapon.GetComponent<BaseWeapon>();
            this._weapon.OnUnequiped();

            this._utility = this._objectUtility.GetComponent<BaseUtility>();
            this._utility.OnUnequiped();

            this.EquipWeapon();
        }

        public void EquipUtility(bool hideWeapon = true)
        {
            if (hideWeapon) this._weapon.OnUnequiped();
            this._utility.OnEquiped();
        }

        public void EquipWeapon()
        {
            this._utility.OnUnequiped();
            this._weapon.OnEquiped();
        }
    }
}