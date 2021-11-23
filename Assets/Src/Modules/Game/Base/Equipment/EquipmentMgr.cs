using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Weapons;
using Character;

namespace Equipments
{
    public class EquipmentMgr : MonoBehaviour
    {
        [SerializeField] GameObject _owner;
        public GameObject Owner => this._owner;

        [SerializeField] GameObject _objectWeapon;
        [SerializeField] GameObject _objectUtility;

        [SerializeField] GameObject _reserveWeapon;

        bool _isUsingWeapon;

        BaseWeapon _weapon;
        public BaseWeapon Weapons => this._weapon;

        BaseUtility _utility;
        public BaseUtility Utility => this._utility;

        void Awake()
        {
            this._weapon = this._objectWeapon.GetComponent<BaseWeapon>();
            this._weapon.Owner = this._owner;
            this._weapon.OnUnequiped();

            this._utility = this._objectUtility.GetComponent<BaseUtility>();
            this._utility.Owner = this._owner;
            this._utility.OnUnequiped();

            this.EquipWeapon();
            this.SubEvents();
        }

        public void EquipUtility(bool hideWeapon = true)
        {
            if (hideWeapon) this._weapon.OnUnequiped();
            this._utility.OnEquiped();

            this._isUsingWeapon = hideWeapon;
        }

        public void EquipWeapon()
        {
            this._isUsingWeapon = true;
            this._utility.OnUnequiped();
            this._weapon.OnEquiped();
        }

        public void EquipLasted()
        {
            if (this._isUsingWeapon) this.EquipWeapon();
            else this.EquipUtility();
        }

        public void UnequipAll()
        {
            this._utility.OnUnequiped();
            this._weapon.OnUnequiped();
        }

        public void SetWeaponToReserved()
        {
            this.UnequipAll();
            this._weapon = this._reserveWeapon.GetComponent<BaseWeapon>();
            this._weapon.Owner = this._owner;
            this._weapon.OnUnequiped();
            this.EquipWeapon();
        }

        void SubEvents()
        {
            this.SubEventCat();
            this.SubEventInteract();
        }

        void SubEventCat()
        {
            CharacterStats stats = this._owner.GetComponent<CharacterStats>();
            if (stats != null && stats.CharacterSide == CharacterSide.CATS)
            {
                EventCenter.Subcribe(EventId.CAT_DOWN, (object pubData) =>
                {
                    PubData.CatDown data = pubData as PubData.CatDown;
                    if (data.Dispatcher == this._owner) this.UnequipAll();
                });

                EventCenter.Subcribe(EventId.CAT_RECOVERED, (object pubData) =>
                {
                    GameObject dispatcher = pubData as GameObject;
                    if (dispatcher == this._owner) this.EquipLasted();
                });

                EventCenter.Subcribe(EventId.CAT_DYING, (object pubData) =>
                {
                    GameObject dispatcher = pubData as GameObject;
                    if (dispatcher == this._owner) this.SetWeaponToReserved();
                });
            }
        }

        void SubEventInteract()
        {
            EventCenter.Subcribe(EventId.INTERACT_START, (object pubData) =>
            {
                PubData.IneractStart data = pubData as PubData.IneractStart;
                if (data.Dispatcher == this.Owner) this.UnequipAll();
            });

            EventCenter.Subcribe(EventId.INTERACT_END, (object pubData) =>
            {
                PubData.InteractEnd data = pubData as PubData.InteractEnd;
                if (data.Dispatcher == this.Owner) this.EquipLasted();
            });
        }
    }
}