using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Utilities
{
    public class HoldUtility : BaseUtility
    {
        protected virtual bool Hold => InputMgr.UseUtil;

        protected void Update()
        {
            //hold to equip and active object
            if (this.Hold)
            {
                if (!this.IsEquiped) this.EquipUtil();
                this.ActiveUtil();
            }
            else {
                this.UnequipUtil();
            }
        }
    }
}
