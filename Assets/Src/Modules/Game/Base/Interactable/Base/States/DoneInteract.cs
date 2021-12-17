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
                PubData.InteractEnd pubData = new PubData.InteractEnd(this.InteractPlayer, this._gameObject, true);
                EventCenter.Publish(EventId.INTERACT_END, pubData);
                NetworkGame.Publish(EventId.INTERACT_END, pubData.Serialize());

                (this._stateMachine as InteractEngine).OnInteractSuccesful();
                this.SetNextState(new Idle(this._stateMachine));
            }
        }
    }
}
