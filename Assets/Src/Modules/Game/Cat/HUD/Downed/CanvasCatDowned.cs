using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Character;

namespace Cats
{
    public class CanvasCatDowned : MonoBehaviour
    {
        GameObject _player;

        UiHelper uiHelper = null;
        Slider _slider = null;
        Text _lbTime = null;

        float _downTime;
        float _timeRemain;

        void Start()
        {
            this._player = GameVar.Ins.Player;

            this.uiHelper = new UiHelper(this.gameObject);
            this._slider = uiHelper.ui[SLIDER].GetComponent<Slider>();
            this._lbTime = uiHelper.ui[LB_TIME].GetComponent<Text>();

            this.HideView();
            EventCenter.Subcribe(EventId.CAT_DOWN, this.ShowView);
            EventCenter.Subcribe(EventId.CAT_RECOVERED, this.HideView);

            this.EnableIfPlayerIsCat();
        }

        void EnableIfPlayerIsCat()
        {
            CharacterStats stats = this._player == null ? null : this._player.GetComponent<CharacterStats>();
            if (this._player == null || stats == null || stats.CharacterSide != CharacterSide.CATS)
            {
                Destroy(this.gameObject);
            }
        }

        void Update()
        {
            if (this._timeRemain > 0)
            {
                this._timeRemain -= Time.deltaTime;
                float ratio = this._timeRemain / this._downTime;
                this._slider.value = ratio;
                this._lbTime.text = this._timeRemain.ToString("F2");
            }
            else this.HideView();
        }

        void ShowView(object pubData)
        {
            PubData.CatDown data = pubData as PubData.CatDown;

            if (data.Dispatcher == this._player)
            {
                this._downTime = data.DownTime;
                this._timeRemain = this._downTime;
                this._slider.gameObject.SetActive(true);
                this._lbTime.gameObject.SetActive(true);
            }
        }

        void HideView(object pubData)
        {
            GameObject dispatcher = pubData as GameObject;
            if (dispatcher == this._player)
            {
                this.HideView();
            }
        }

        void HideView()
        {
            this._timeRemain = 0;
            this._slider.gameObject.SetActive(false);
            this._lbTime.gameObject.SetActive(false);
        }

        const string SLIDER = "Slider";
        const string LB_TIME = "LbTime";
    }
}
