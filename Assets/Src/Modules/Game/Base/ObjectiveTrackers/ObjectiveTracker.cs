using System.Collections;
using System.Collections.Generic;
using PubData;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    static ObjectiveTracker _ins;
    public static ObjectiveTracker Ins => _ins;

    [SerializeField] List<GameObject> _cucumbers;
    public List<GameObject> Cucumbers => this._cucumbers;

    void Awake()
    {
        _ins = this;
        EventCenter.Subcribe(EventId.INTERACT_END, this.OnEndInteractObject);
    }

    void OnEndInteractObject(object pubData)
    {
        InteractEnd data = pubData as InteractEnd;
        if (data.IsSuccessful && this._cucumbers.Contains(data.InteractObject))
        {
            Character.CharacterStats stats = data.Dispatcher.GetComponent<Character.CharacterStats>();
            if (stats.CharacterSide == CharacterSide.MICE)
            {
                this._cucumbers.Remove(data.InteractObject);
                //if there is no more cucumber, end game and mouse win
                if (this._cucumbers.Count == 0)
                {
                    EventCenter.Publish(
                        EventId.MATCH_END,
                        new MatchEnd(CharacterSide.MICE, WinReason.MICE_OBTAIN_ALL)
                    );
                }
            }
        }
    }
}