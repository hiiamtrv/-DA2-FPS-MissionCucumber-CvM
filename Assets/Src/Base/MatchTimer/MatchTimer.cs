using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    [SerializeField] float _matchSeconds;

    float _remainTime;
    public float RemainTime => this._remainTime;

    static bool _isPaused;
    public static bool IsPaused => _isPaused;

    static bool _isStopped;
    public static bool IsStopped => _isStopped;

    void Awake()
    {
        this._remainTime = this._matchSeconds;
        EventCenter.Subcribe(EventId.TIMER_END, this.OnTimerEnd);
    }

    void Start()
    {
        this.Play();
    }

    void LateUpdate()
    {
        if (!_isPaused && !_isStopped)
        {
            this._remainTime -= Time.deltaTime;
            if (this._remainTime <= 0)
            {
                this.Stop();
                EventCenter.Publish(
                    EventId.MATCH_END,
                    new PubData.MatchEnd(PlayerSide.CATS, WinReason.TIME_OUT)    
                );
                EventCenter.Publish(EventId.TIMER_END, true);
            }
        }
    }

    void Pause()
    {
        _isPaused = true;
        EventCenter.Publish(EventId.TIMER_END, false);
    }

    void Play()
    {
        _isPaused = false;
        EventCenter.Publish(EventId.TIMER_START, this._remainTime);
    }

    void Stop()
    {
        _isStopped = true;
    }

    void OnTimerEnd(object pubData)
    {
        _isStopped = (bool)pubData;
    }
}
