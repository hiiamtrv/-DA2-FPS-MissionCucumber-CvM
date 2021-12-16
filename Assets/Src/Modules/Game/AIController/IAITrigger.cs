using System.Collections.Generic;
using UnityEngine;
using PubData;

namespace AI
{
    public interface IAITrigger
    {
        public void OnEndAction();

        public void OnSpotEnemy(List<GameObject> enemies);

        public void OnLostTarget(GameObject target);

        public void OnTargetDead(GameObject target);

        public void OnMeetInteractable(List<IInteractable> interactables);

        public void OnDamaged(GameObject attacker);

        public void OnShieldOut();
    }
}