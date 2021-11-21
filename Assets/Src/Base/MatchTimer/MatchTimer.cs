using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    [SerializeField] float _matchSeconds;

    float _remainTime;
    public float RemainTime => this._remainTime;

    bool _isPaused;
    public bool IsPause => this._isPaused;

    void Awake()
    {
        this._remainTime = this._matchSeconds;
    }

    void Start()
    {
        this.Play();
    }

    void LateUpdate()
    {
        if (!this._isPaused)
        {
            this._remainTime -= Time.deltaTime;
            if (this._remainTime <= 0)
            {
                EventCenter.Publish(EventId.TIMER_END, true);
                this.Pause();
            }
        }
    }

    void Pause()
    {
        this._isPaused = true;
        EventCenter.Publish(EventId.TIMER_END, false);
    }

    void Play()
    {
        this._isPaused = false;
        Debug.LogFormat("Start event timer start {0}", this._remainTime);
        EventCenter.Publish(EventId.TIMER_START, this._remainTime);
    }
}
