using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using BayatGames.Serialization.Formatters.Json;
using PubData;

public class SyncEvent : MonoBehaviour
{
    static SyncEvent _ins;

    public static void Publish(string eventId, object data)
    {
        Debug.Log("Publish Sync event", eventId, data);
        _ins._view.RPC("Pub", RpcTarget.OthersBuffered, new object[] { eventId, data });
    }

    PhotonView _view;

    void Awake()
    {
        _ins = this;
        _view = this.GetComponent<PhotonView>();
    }

    [PunRPC]
    void Pub(object[] data)
    {
        string eventId = (string)data[0];
        object pubData = this.DeserializePubData(eventId, (object)data[1]);
        EventCenter.Publish(eventId, pubData);
    }

    object DeserializePubData(string eventId, object data)
    {
        switch (eventId)
        {
            case EventId.HEALTH_CHANGE: return HealthChange.Deserialize(data as object[]);
            case EventId.SHILED_CHANGE: return ShieldChange.Deserialize(data as object[]);
            case EventId.INTERACT_REQUEST: return InteractRequest.Deserialize(data as object[]);
            default: return data;
        }
    }
}
