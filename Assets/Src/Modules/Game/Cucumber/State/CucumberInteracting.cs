using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable.State;
using Character;

namespace Cucumbers
{
    public class CucumberInteracting : Interacting
    {
        protected CucumberEngine StateMachine => ((CucumberEngine)this._stateMachine);
        protected CucumberModel Model => StateMachine.Model as CucumberModel;

        public CucumberInteracting(StateMachine stateMachine) : base(stateMachine)
        {
            CharacterStats stats = this.StateMachine.InteractPlayer.GetComponent<CharacterStats>();
            if (stats != null)
            {
                float interactTime = this.Model.GetInteractTime(stats.CharacterSide);
                this.Model.InteractTime = interactTime;
            }
        }
    }
}
