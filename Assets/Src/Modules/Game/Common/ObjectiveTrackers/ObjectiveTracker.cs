using System.Collections;
using System.Collections.Generic;
using PubData;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    [SerializeField] List<GameObject> _cucumbers;
    public List<GameObject> Cucumbers => this._cucumbers;

    [SerializeField] GameObject _shieldCenter;
    public GameObject ShieldCenter => this._shieldCenter;

    void Awake()
    {
        EventCenter.Subcribe(EventId.INTERACT_END, this.OnEndInteractObject);
    }

    void OnEndInteractObject(object pubData)
    {
        InteractEnd data = pubData as InteractEnd;
        if (data.IsSuccessful && this._cucumbers.Contains(data.InteractObject))
        {
            this._cucumbers.Remove(data.InteractObject);
            //if there is no more cucumber, end game and mouse win
            if (this._cucumbers.Count == 0)
            {
                EventCenter.Publish(
                    EventId.MATCH_END,
                    new MatchEnd(PlayerSide.MICE, WinReason.MICE_OBTAIN_ALL)
                );
                EventCenter.Publish(EventId.TIMER_END, true);
            }
        }
    }
}