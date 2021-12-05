using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameHud
{
    public class CanvasInteract : MonoBehaviour
    {
        GameObject _player;

        UiHelper uiHelper = null;
        Slider _slider = null;
        Text _lbTime = null;

        bool _isShowing = false;
        float _curTime = 0;
        float _endTime = 0;

        void Awake()
        {
            EventCenter.Subcribe(EventId.CREATE_PLAYER, (data) =>
            {
                this._player = GameVar.Ins.Player;
            });
            this.SubEvents();
        }

        void Start()
        {
            this.uiHelper = new UiHelper(this.gameObject);
            this._slider = uiHelper.ui[SLIDER].GetComponent<Slider>();
            this._lbTime = uiHelper.ui[LB_TIME].GetComponent<Text>();

            this.HideInteracTimer();
        }

        void Update()
        {
            if (this._isShowing)
            {
                this._curTime += Time.deltaTime;
                float ratio = this._curTime / this._endTime;

                this._slider.value = ratio;
                this._lbTime.text = this._curTime.ToString("F2");
            }
        }

        void SubEvents()
        {
            EventCenter.Subcribe(
                EventId.INTERACT_START,
                (object pubData) =>
                {
                    PubData.IneractStart data = pubData as PubData.IneractStart;
                    if (data.Dispatcher == this._player)
                    {
                        float startTime = data.StartTime;
                        float endTime = data.InteractModel.InteractTime;
                        this.ShowInteractTimer(startTime, endTime);
                    }
                }
            );

            EventCenter.Subcribe(
                EventId.INTERACT_END,
                (object pubData) =>
                {
                    PubData.InteractEnd data = pubData as PubData.InteractEnd;
                    if (data.Dispatcher == this._player) this.HideInteracTimer();
                }
            );
        }

        void ShowInteractTimer(float startTime, float endTime)
        {
            this._isShowing = true;
            this._curTime = startTime;
            this._endTime = endTime;
            this._slider.gameObject.SetActive(true);
            this._lbTime.gameObject.SetActive(true);
        }

        void HideInteracTimer()
        {
            this._isShowing = false;
            this._curTime = 0;
            this._endTime = 0;
            this._slider.gameObject.SetActive(false);
            this._lbTime.gameObject.SetActive(false);
        }

        const string SLIDER = "Slider";
        const string LB_TIME = "LbTime";
    }
}
