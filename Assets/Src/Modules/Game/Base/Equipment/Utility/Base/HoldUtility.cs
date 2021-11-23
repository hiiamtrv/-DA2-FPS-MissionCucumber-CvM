using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Utilities
{
    public class HoldUtility : BaseUtility
    {
        protected virtual bool StartUse => InputMgr.StartUseUtil;
        protected virtual bool EndUse => InputMgr.EndUseUtil;

        protected bool _isUsing;

        protected override void Start()
        {
            this._isUsing = false;
            base.Start();
        }

        protected void Update()
        {
            if (this.StartUse)
            {
                if (!this.IsEquiped) this.EquipUtil();
                this._isUsing = true;
            }

            if (this.EndUse)
            {
                this.UnequipUtil();
                this._isUsing = false;
            }

            if (this._isUsing) this.ActiveUtil();
        }
    }
}
