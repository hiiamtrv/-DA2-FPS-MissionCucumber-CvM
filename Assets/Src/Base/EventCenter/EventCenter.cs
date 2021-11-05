using System;
using System.Collections.Generic;
using UnityEngine;

public partial class EventCenter
{
    public static void Subcribe(string eventId, Action<object> action)
    {
        GetInstance().AddSubriber(eventId, action);
    }

    public static void Unsubcribe(string eventId, Action<object> action)
    {
        GetInstance().RemoveSubcriber(eventId, action);
    }

    public static void Publish(string eventId, object data)
    {
        GetInstance().Notify(eventId, data);
    }

    public static void Publish(string eventId)
    {
        GetInstance().Notify(eventId, null);
    }
}

#region PRIVATE METHODS
public partial class EventCenter
{
    static EventCenter _instance = null;
    private static EventCenter GetInstance()
    {
        if (_instance == null)
        {
            _instance = new EventCenter();
        }
        return _instance;
    }

    Dictionary<String, List<Action<object>>> _eventMap;

    EventCenter()
    {
        _eventMap = new Dictionary<String, List<Action<object>>>();
    }

    List<Action<object>> GetSubribers(string eventId)
    {
        if (!_eventMap.ContainsKey(eventId))
        {
            _eventMap.Add(
                eventId,
                new List<Action<object>>()
            );
        }
        _eventMap[eventId] = _eventMap[eventId].FindAll(action => action != null);
        return _eventMap[eventId];
    }

    void AddSubriber(string eventId, Action<object> action)
    {
        List<Action<object>> subcribers = this.GetSubribers(eventId);
        subcribers.Add(action);
    }

    void RemoveSubcriber(string eventId, Action<object> action)
    {
        List<Action<object>> subcribers = this.GetSubribers(eventId);
        subcribers.Remove(action);
    }

    void Notify(string eventId, object data)
    {
        var subcribers = GetInstance().GetSubribers(eventId);
        foreach (Action<object> action in subcribers)
        {
            action(data);
        }
    }
}
#endregion