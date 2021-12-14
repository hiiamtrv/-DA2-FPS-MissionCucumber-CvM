using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PubData;
using Character;

namespace GameHud
{
    public class CanvasObjectiveProcess : MonoBehaviour
    {
        UiHelper uiHelper = null;
        Slider _sliderMouse;
        Slider _sliderCat;
        Text _lbLastCucumber;
        Slider _slider;

        GameObject _player;
        int _cucumberObtained;

        void Awake()
        {
            EventCenter.Subcribe(EventId.CREATE_PLAYER, (data) =>
            {
                this._player = GameVar.Ins.Player;
                CharacterSide side = this._player.GetComponent<CharacterStats>().CharacterSide;
                Debug.Log("Get character side", side);

                this._sliderCat.gameObject.SetActive(side == CharacterSide.CATS);
                this._sliderMouse.gameObject.SetActive(side == CharacterSide.MICE);
                this._cucumberObtained = 0;
            });

            EventCenter.Subcribe(EventId.INTERACT_END, (pubData) =>
            {
                InteractEnd data = pubData as InteractEnd;
                Debug.Log("Interact end", data.IsSuccessful, data.InteractObject, ObjectiveTracker.Ins.IsCucumber(data.InteractObject));
                if (data.IsSuccessful && ObjectiveTracker.Ins.IsCucumber(data.InteractObject))
                {
                    this._cucumberObtained++;
                    this._sliderCat.value = (float)this._cucumberObtained / NetworkGame.NUM_CUCUMBER_ON_FIELD;
                    this._sliderMouse.value = (float)this._cucumberObtained / NetworkGame.NUM_CUCUMBER_ON_FIELD;
                    if (this._cucumberObtained == NetworkGame.NUM_CUCUMBER_ON_FIELD - 1)
                    {
                        this._lbLastCucumber.gameObject.SetActive(true);
                    }
                    Debug.Log("Cucumber obtained", this._cucumberObtained, this._sliderMouse.value, this._sliderCat.value);
                }
            });
        }

        void Start()
        {
            this.uiHelper = new UiHelper(this.gameObject);
            this._sliderCat = uiHelper.ui[SLIDER_CAT].GetComponent<Slider>();
            this._sliderMouse = uiHelper.ui[SLIDER_MOUSE].GetComponent<Slider>();
            this._lbLastCucumber = uiHelper.ui[LB_LAST_CUCUMBER].GetComponent<Text>();

            this._lbLastCucumber.gameObject.SetActive(false);
            
            this._sliderCat.value = 0;
            this._sliderMouse.value = 0;
        }

        const string SLIDER_MOUSE = "SliderMouse";
        const string SLIDER_CAT = "SliderCat";
        const string LB_LAST_CUCUMBER = "LbLastCucumber";
    }
}
