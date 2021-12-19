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

        Text _lbNumAlly;
        Text _lbNumEnemy;
        Text _lbAlly;
        Text _lbEnemy;

        GameObject _player;
        int _cucumberObtained;

        CharacterSide ally;
        CharacterSide enemy;

        int _numAlly;
        int _numEnemy;

        void Awake()
        {
            // EventCenter.Subcribe(EventId.CREATE_PLAYER, (data) =>
            // {
            //     this._player = GameVar.Ins.Player;
            //     CharacterSide side = this._player.GetComponent<CharacterStats>().CharacterSide;
            //     Debug.Log("Get character side", side);

            //     this._sliderCat.gameObject.SetActive(side == CharacterSide.CATS);
            //     this._sliderMouse.gameObject.SetActive(side == CharacterSide.MICE);
            //     this._cucumberObtained = 0;
            // });

            EventCenter.Subcribe(EventId.CUCUMBER_OBTAINED, (pubData) =>
            {
                this._cucumberObtained++;
                this._sliderCat.value = (float)this._cucumberObtained / NetworkGame.NUM_CUCUMBER_ON_FIELD;
                this._sliderMouse.value = (float)this._cucumberObtained / NetworkGame.NUM_CUCUMBER_ON_FIELD;
                if (this._cucumberObtained == NetworkGame.NUM_CUCUMBER_ON_FIELD - 1)
                {
                    this._lbLastCucumber.gameObject.SetActive(true);
                }
            });

            EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, (e) =>
            {
                GameObject dieObject = e as GameObject;
                CharacterSide side = dieObject.GetComponent<CharacterStats>().CharacterSide;
                if (side == GameVar.StartSide) _numAlly--;
                if (side != GameVar.StartSide) _numEnemy--;
                this.UpdateCharNumber();
            });
        }

        void Start()
        {
            this.uiHelper = new UiHelper(this.gameObject);
            this._sliderCat = uiHelper.ui[SLIDER_CAT].GetComponent<Slider>();
            this._sliderMouse = uiHelper.ui[SLIDER_MOUSE].GetComponent<Slider>();
            this._lbLastCucumber = uiHelper.ui[LB_LAST_CUCUMBER].GetComponent<Text>();

            this._lbAlly = uiHelper.ui[LB_ALLY].GetComponent<Text>();
            this._lbEnemy = uiHelper.ui[LB_ENEMY].GetComponent<Text>();
            this._lbNumAlly = uiHelper.ui[LB_NUM_ALLY].GetComponent<Text>();
            this._lbNumEnemy = uiHelper.ui[LB_NUM_ENEMY].GetComponent<Text>();
            
            this._lbLastCucumber.gameObject.SetActive(false);

            this._sliderCat.value = 0;
            this._sliderMouse.value = 0;
        }

        void Update()
        {
            if (this._player == null && GameVar.Ins.Player != null)
            {
                this._player = GameVar.Ins.Player;
                CharacterSide side = this._player.GetComponent<CharacterStats>().CharacterSide;

                this._sliderCat.gameObject.SetActive(side == CharacterSide.CATS);
                this._sliderMouse.gameObject.SetActive(side == CharacterSide.MICE);
                this._cucumberObtained = 0;

                switch (side)
                {
                    case CharacterSide.CATS:
                        this._numAlly = NetworkGame.NUM_CATS_SLOT;
                        this._numEnemy = NetworkGame.NUM_MICE_SLOT;
                        this._lbAlly.text = "Cats";
                        this._lbEnemy.text = "Mouse";
                        this.UpdateCharNumber();
                        break;
                    case CharacterSide.MICE:
                        this._numEnemy = NetworkGame.NUM_CATS_SLOT;
                        this._numAlly = NetworkGame.NUM_MICE_SLOT;
                        this._lbEnemy.text = "Cats";
                        this._lbAlly.text = "Mouse";
                        this.UpdateCharNumber();
                        break;
                }
            }
        }

        void UpdateCharNumber()
        {
            this._lbNumAlly.text = _numAlly.ToString();
            this._lbNumEnemy.text = _numEnemy.ToString();
        }

        const string SLIDER_MOUSE = "SliderMouse";
        const string SLIDER_CAT = "SliderCat";
        const string LB_LAST_CUCUMBER = "LbLastCucumber";
        const string LB_NUM_ALLY = "LbNumAlly";
        const string LB_NUM_ENEMY = "LbNumEnemy";
        const string LB_ALLY = "LbEnemy";
        const string LB_ENEMY = "LbAlly";
    }
}
