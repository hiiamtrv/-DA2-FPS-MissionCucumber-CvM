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

        public override void OnSpotEnemy()
        {
            base.OnSpotEnemy();
        }

        public override void OnLostTarget()
        {
            base.OnLostTarget();
        }

        public override void OnTargetDead()
        {
            base.OnTargetDead();
        }

        public override void OnMeetInteractable()
        {
            base.OnMeetInteractable();
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
