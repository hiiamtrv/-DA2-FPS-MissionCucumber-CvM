using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Utilities
{
    public class HoldUtility : BaseUtility
    {
        protected virtual bool StartUse => InputMgr.StartUseUtil(this._owner);
        protected virtual bool EndUse => InputMgr.EndUseUtil(this._owner);

        protected bool _isUsing;

        protected override void Start()
        {
            this._isUsing = false;
            base.Start();
        }

        protected override void Update()
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

            base.Update();
        }
    }
}
