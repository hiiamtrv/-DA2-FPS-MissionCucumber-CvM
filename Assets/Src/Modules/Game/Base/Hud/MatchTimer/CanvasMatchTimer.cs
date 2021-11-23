using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using Weapons;
using UnityEngine.UI;
using System;

namespace GameHud
{
    public class CanvasMatchTimer : MonoBehaviour
    {
        const string TIME_FORMAT = @"mm\:ss";

        UiHelper uiHelper = null;
        Text _lbTimeDisplay = null;

        bool _isPaused = true;
        bool _isStopped = false;
        float _remainTime;

        void Awake()
        {
            EventCenter.Subcribe(EventId.TIMER_START, this.StartTimer);
            EventCenter.Subcribe(EventId.TIMER_END, this.EndTimer);
        }

        void Start()
        {
            this.uiHelper = new UiHelper(this.gameObject);
            this._lbTimeDisplay = uiHelper.ui[LB_TIME_DISPLAY].GetComponent<Text>();
        }

        void LateUpdate()
        {
            if (!this._isPaused && !this._isStopped)
            {
                this._remainTime -= Time.deltaTime;
                TimeSpan timeDisplay = TimeSpan.FromSeconds((double) this._remainTime);
                this._lbTimeDisplay.text = timeDisplay.ToString(TIME_FORMAT);
            }
            else
            {
                if (this._isStopped) this._lbTimeDisplay.text = "Match Stopped";
                else this._lbTimeDisplay.text = "Match Paused";
            }
        }

        void StartTimer(object pubData)
        {
            this._isPaused = false;
            this._remainTime = (float)pubData;
        }

        void EndTimer(object pubData)
        {
            this._isPaused = false;
            this._isStopped = (bool)pubData;
        }

        const string LB_TIME_DISPLAY = "LbTimeDisplay";
    }
}
