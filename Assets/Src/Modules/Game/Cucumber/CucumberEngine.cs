using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable;
using Character;
using Cats.HealthState;

namespace Cucumbers
{
    public class CucumberEngine : InteractEngine
    {
        public override void OnInteractSuccesful()
        {
            CharacterStats stats = this._interactPlayer.GetComponent<CharacterStats>();
            if (stats)
            {
                switch (stats.CharacterSide)
                {
                    case CharacterSide.CATS:
                        this.ActiveDyingState();
                        break;
                    case CharacterSide.MICE:
                        this.gameObject.SetActive(false);
                        break;
                }
            }
        }

        void ActiveDyingState()
        {
            HealthEngine healthEngine = this._interactPlayer.GetComponent<HealthEngine>();
            CatDying stateDying = new CatDying(healthEngine);
            healthEngine.ChangeState(stateDying);
        }
    }
}
