using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AICatEngine : AIBaseEngine
    {
        public override void OnEndAction()
        {
            base.OnEndAction();
        }

        public override void OnSpotEnemy(List<GameObject> enemies)
        {
            //if there is only 1 enemy, chase that target
            if (enemies.Count == 1)
            {
                ChaseTarget chaseState = new ChaseTarget(this);
                chaseState.SetTarget(enemies);
                this.ChangeState(chaseState);
            }
            //else, chase or retreat up to shield remains
            else
            {

            }
        }

        public override void OnLostTarget(GameObject target)
        {
            base.OnLostTarget(target);
        }

        public override void OnTargetDead(GameObject target)
        {
            base.OnTargetDead(target);
        }

        public override void OnMeetInteractable(List<IInteractable> interactables)
        {
            base.OnMeetInteractable(interactables);
        }

        public override void OnDamaged(GameObject attacker)
        {
            base.OnDamaged(attacker);
        }

        public override void OnShieldOut()
        {
            base.OnShieldOut();
        }

        public override BaseState RollNextState()
        {
            return base.RollNextState();
        }
    }
}
