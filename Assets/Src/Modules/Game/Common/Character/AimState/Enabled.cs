using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    namespace AimState
    {
        public class Enabled : BaseState
        {
            Vector2 _pAimOnScreen;

            public Enabled(StateMachine stateMachine) : base(stateMachine)
            {
                _pAimOnScreen = new Vector2(0.5f, 0.5f);
            }

            public override void LogicUpdate()
            {
                if (InputMgr.Shoot)
                {
                    this.Shoot();
                }
                else if (InputMgr.Interact)
                {
                    this.Interact();
                }

                base.LogicUpdate();
            }

            void Shoot()
            {

            }

            void Interact()
            {

            }

            // GameObject Target => {

            // }
    }
}
}