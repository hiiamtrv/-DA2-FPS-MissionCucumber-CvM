using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable;
using Interactable.State;
using Character;
using Cats;
using Cats.HealthState;

namespace Cucumbers
{
    public class CucumberEngine : InteractEngine
    {
        [SerializeField] AudioClip _mouseObtainedCatSide;
        [SerializeField] AudioClip _mouseObtainedMouseSide;
        [SerializeField] AudioClip _catObtained;

        public CucumberModel Model => this._model as CucumberModel;
        protected override BaseState InteractingState => new CucumberInteracting(this);

        protected override void GetModel()
        {
            this._model = this.GetComponent<CucumberStats>().Model;
        }

        public override void OnInteractSuccesful()
        {
            CharacterStats stats = this._interactPlayer.GetComponent<CharacterStats>();
            if (stats)
            {
                CharacterSide interactorSide = stats.CharacterSide;

                switch (interactorSide)
                {
                    case CharacterSide.CATS:
                        this.ActiveDyingState();
                        break;
                    case CharacterSide.MICE:
                        this.gameObject.SetActive(false);
                        switch (GameVar.StartSide)
                        {
                            case CharacterSide.CATS:
                                GameVar.Ins.Player.PlaySound(_mouseObtainedCatSide);
                                break;
                            case CharacterSide.MICE:
                                GameVar.Ins.Player.PlaySound(_mouseObtainedMouseSide);
                                break;
                        }
                        break;
                }
            }
        }

        void ActiveDyingState()
        {
            CatHealthEngine healthEngine = this._interactPlayer.GetComponent<CatHealthEngine>();
            CatDying stateDying = new CatDying(healthEngine);
            healthEngine.ChangeState(stateDying);

            this._interactPlayer.PlaySound(this._catObtained);
        }
    }
}
