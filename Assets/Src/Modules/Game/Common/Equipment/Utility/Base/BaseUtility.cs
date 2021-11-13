using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Utilities
{
    public class BaseUtility : Equipment
    {
        [SerializeField] UtilName _name;
        public UtilName Name => this._name;

        public virtual bool HideGunWhenUse => true;

        protected virtual void EquipUtil()
        {
            //TODO: inherit if you want to do somewthing before equip the utility
            this.EquipMgr.EquipUtility(this.HideGunWhenUse);
        }

        protected virtual void UnequipUtil()
        {
            this.EquipMgr.EquipWeapon();
        }

        protected virtual void ActiveUtil()
        {
            //TODO: must inherit and do something else
        }
    }
}
