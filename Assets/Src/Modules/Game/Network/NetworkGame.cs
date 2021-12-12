using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PubData;
using UnityEngine.SceneManagement;

public class NetworkGame : BaseNetwork
{
    public const int NUM_PLAYER_CATS = 1;
    public const int NUM_PLAYER_MICE = 1;

    public const int NUM_CATS_SLOT = 2;
    public const int NUM_MICE_SLOT = 4;

    public const int NUM_CUCUMBER_POINT = 10;
    public const int NUM_CUCUMBER_ON_FIELD = 5;

    static NetworkGame _ins;
    public static NetworkGame Ins => _ins;
    static PhotonView _view;

    public static void Publish(string eventId, object data)
    {
        Debug.Log("Publish Sync event", eventId, data);
        _view.RPC(nameof(Pub), RpcTarget.OthersBuffered, new object[] { eventId, data });
    }

    protected override void Awake()
    {
        base.Awake();
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

    public void EndGame()
    {
        SceneManager.UnloadSceneAsync(SceneId.GAMEDEMO);
        EventCenter.Renew();
        SceneManager.LoadScene(SceneId.LOBBY);
    }
}
