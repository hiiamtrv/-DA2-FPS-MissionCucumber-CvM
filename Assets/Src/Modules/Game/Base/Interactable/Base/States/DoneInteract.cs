using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    namespace State
    {
        public class DoneInteract : BaseState
        {
            public GameObject InteractPlayer => ((InteractEngine)this._stateMachine).InteractPlayer;

            public DoneInteract(StateMachine stateMachine) : base(stateMachine) { }

            public override void OnEnter()
            {
                // this._stateMachine.Destroy();
                EventCenter.Publish(
                        EventId.INTERACT_END,
                        new PubData.InteractEnd(this.InteractPlayer, this._gameObject, true)
                    );
                this._gameObject.SetActive(false);
            }
        }
    }
}
