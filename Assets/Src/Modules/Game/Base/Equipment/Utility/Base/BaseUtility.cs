using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;
using PubData;

namespace Utilities
{
    public class BaseUtility : Equipment
    {
        [SerializeField] protected AudioClip _equipSound;

        //TODO: Override utility name to name another name
        protected UtilityModel _model;
        public UtilityModel Model => _model;

        protected float _curCooldown;
        public float Cooldown => this._curCooldown;
        public virtual bool IsOnCooldown => this._curCooldown > Time.deltaTime;

        public virtual bool HideGunWhenUse => true;

        protected override void Start()
        {
            this.GetModel();
            base.Start();
        }

        protected virtual void Update()
        {
            this._curCooldown = Mathf.Max(0, this._curCooldown - Time.deltaTime);
        }

        protected virtual void EquipUtil()
        {
            if (this.IsOnCooldown) return;

            this.gameObject.PlaySound(_equipSound);
            //TODO: inherit if you want to do somewthing before equip the utility, remember this.IsOnCooldown
            this.EquipMgr.EquipUtility(this.HideGunWhenUse);
            Crosshair.Ins.SetVisible(this.Model.EnableCrosshair);
        }

        protected virtual void UnequipUtil()
        {
            this.EquipMgr.EquipWeapon();
            Crosshair.Ins.SetVisible(!this.Model.EnableCrosshair);
        }

        protected virtual void GetModel()
        {
            this._model = this.GetComponent<UtilityStats>().Model;
            //TODO: must override if Stats or Model have differences
        }

        protected virtual void ActiveUtil()
        {
            this._curCooldown = this.Model.Cooldown;
            EventCenter.Publish(
                EventId.UTILITY_START_COOLDOWN,
                new UtilityStartCooldown(this._owner, this._curCooldown)
            );

            //TODO: must inherit and do something else
        }
    }
}
