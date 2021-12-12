using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace AI
{
    public class AIMouseEngine : AIBaseEngine
    {
        public override void OnEndAction()
        {
            base.OnEndAction();
        }

        public override void OnSpotEnemy(List<GameObject> enemies)
        {
            //if there is only 1 enemy
            if (enemies.Count == 1)
            {
                GameObject enemy = enemies[0];
                float shieldRemain = enemy.GetComponent<HealthEngine>().Model.Shield;

                //attack if the enemy has no shield
                if (shieldRemain <= 0)
                {
                    ChaseTarget chaseState = new ChaseTarget(this);
                    chaseState.SetTarget(enemies);
                    this.ChangeState(chaseState);
                }
                //else, retreat
                else
                {
                    Retreat retreatState = new Retreat(this);
                    retreatState.Chaser = enemy;
                    this.ChangeState(retreatState);
                }
            }
            //else auto-retreat
            else
            {
                Retreat retreatState = new Retreat(this);
                retreatState.Chaser = enemies[0];
                this.ChangeState(retreatState);
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

        public override void OnMeetInteractable(GameObject interactObject)
        {
            base.OnMeetInteractable(interactObject);
        }

        public override void OnDamaged()
        {
            base.OnDamaged();
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
