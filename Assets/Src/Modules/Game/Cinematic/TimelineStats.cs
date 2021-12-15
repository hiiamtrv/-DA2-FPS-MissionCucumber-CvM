using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TimelineStats : MonoBehaviour
{
    [SerializeField] float _timeScale;
    TimelineAsset timeline;

    void Awake()
    {
        timeline = this.GetComponent<TimelineAsset>();

        foreach (var track in timeline.GetOutputTracks())
        {
            foreach (var clip in track.GetClips()) {
                clip.timeScale = _timeScale;
            }
        }
    }
}
