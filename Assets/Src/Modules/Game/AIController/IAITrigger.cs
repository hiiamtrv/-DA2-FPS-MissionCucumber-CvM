using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public interface IAITrigger
    {
        public void OnEndAction();

        public void OnSpotEnemy(List<GameObject> enemies);

        public void OnLostTarget(GameObject target);

        public void OnTargetDead(GameObject target);

        public void OnMeetInteractable(GameObject interactObject);

        public void OnDamaged();

        public void OnShieldOut();
    }
}