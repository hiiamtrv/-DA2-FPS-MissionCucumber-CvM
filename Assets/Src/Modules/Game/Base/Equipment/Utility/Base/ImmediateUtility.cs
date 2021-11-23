using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Utilities
{
    public class ImmediateUtility : BaseUtility
    {
        [SerializeField] float duration;
        protected virtual bool Active => InputMgr.StartUseUtil;

        protected void Update()
        {
            //hold to equip and active object
            if (this.Active)
            {
                if (!this.IsEquiped) this.EquipUtil();
                this.ActiveUtil();

                LeanTween.delayedCall(duration, () =>
                {
                    if (this != null && this.IsEquiped) this.UnequipUtil();
                });
            }
        }
    }
}
