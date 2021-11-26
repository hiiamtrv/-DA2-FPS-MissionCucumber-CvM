using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Utilities
{
    public class BaseUtility : Equipment
    {
        //TODO: Override utility name to name another name
        UtilityModel _model;
        public UtilityModel Model => _model;

        public virtual bool HideGunWhenUse => true;

        protected override void Start()
        {
            this.GetModel();
            base.Start();
        }

        protected virtual void EquipUtil()
        {
            //TODO: inherit if you want to do somewthing before equip the utility
            this.EquipMgr.EquipUtility(this.HideGunWhenUse);
        }

        protected virtual void UnequipUtil()
        {
            this.EquipMgr.EquipWeapon();
        }

        protected virtual void GetModel()
        {
            this._model = this.GetComponent<UtilityStats>().Model;
            //TODO: must override if Stats or Model have differences
        }

        protected virtual void ActiveUtil()
        {
            //TODO: must inherit and do something else
        }
    }
}
