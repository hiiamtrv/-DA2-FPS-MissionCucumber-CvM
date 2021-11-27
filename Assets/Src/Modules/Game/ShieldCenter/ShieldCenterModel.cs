using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable;

namespace ShieldCenter
{
    public class ShieldCenterModel : InteractModel
    {
        List<float> _checkPoints;

        public ShieldCenterModel(float interactTime, bool canMoveWhileInteract, float interactRadius, List<float> checkPoints)
            : base(interactTime, canMoveWhileInteract, interactRadius)
        {
            this._checkPoints = checkPoints;
            this._checkPoints.Sort();
        }

        public float GetLastedCheckPoint(float lastProgress)
        {
            float lastedCheckPoint = 0;
            foreach (float checkPoint in this._checkPoints)
            {
                if (checkPoint <= lastProgress) lastedCheckPoint = checkPoint;
                else break;
            }
            return lastedCheckPoint;
        }
    }
}
