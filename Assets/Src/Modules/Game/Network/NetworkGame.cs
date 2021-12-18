using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PubData;
using UnityEngine.SceneManagement;
using Character;
using Character.HealthState;
using BayatGames.Serialization.Formatters.Json;

public class NetworkGame : BaseNetwork
{
    public const int NUM_CATS_SLOT = 2;
    public const int NUM_MICE_SLOT = 4;

    public const int NUM_CUCUMBER_POINT = 10;
    public const int NUM_CUCUMBER_ON_FIELD = 5;

    static NetworkGame _ins;
    public static NetworkGame Ins => _ins;
    static PhotonView _view;

    static List<object> _falseData;

    public static void Publish(string eventId, object data, bool pubEvCenter = false)
    {
        Debug.Log("Publish Sync event", eventId, data.ToJson());
        _view.RPC(nameof(Pub), RpcTarget.OthersBuffered, new object[] { eventId, data });
    }

    public static void Publish(string eventId, bool pubEvCenter = false)
    {
        Debug.Log("Publish Sync event", eventId);
        _view.RPC(nameof(Pub), RpcTarget.OthersBuffered, new object[] { eventId, null });
    }

    public static void AddFalseData(object data)
    {
        _falseData.Add(data);
    }

    protected bool IsDataFalse(object data)
    {
        bool isFale = _falseData.Find(item => object.ReferenceEquals(item, data)) != default(object);
        if (isFale) _falseData = _falseData.FindAll(item => !object.ReferenceEquals(item, data));
        return isFale;
    }

    protected override void Awake()
    {
        base.Awake();
        _ins = this;
        _view = this.GetComponent<PhotonView>();
        _falseData = new List<object>();
    }

    [PunRPC]
    void Pub(object[] data)
    {
        Debug.Log("Received Sync event", data[0], data[1]);
        string eventId = (string)data[0];
        object pubData = (data[1] == null ? null : this.DeserializePubData(eventId, (object)data[1]));

        if (!this.IsDataFalse(pubData)) EventCenter.Publish(eventId, pubData);
    }

    object DeserializePubData(string eventId, object data)
    {
        object[] arrData = data as object[];
        switch (eventId)
        {
            case EventId.HEALTH_CHANGE: return HealthChange.Deserialize(arrData);
            case EventId.SHILED_CHANGE: return ShieldChange.Deserialize(arrData);
            case EventId.INTERACT_REQUEST: return InteractRequest.Deserialize(arrData);
            case EventId.CAT_DOWN: return CatDown.Deserialize(arrData);
            case EventId.INTERACT_START: return IneractStart.Deserialize(arrData);
            case EventId.INTERACT_END: return InteractEnd.Deserialize(arrData);
            case EventId.CHARACTER_ELIMINATED: return PhotonView.Find((int)arrData[0]);
            default: return data;
        }
    }

    public void EndGame()
    {
        SceneManager.UnloadSceneAsync(SceneId.GAMEDEMO);
        EventCenter.Renew();
        SceneManager.LoadScene(SceneId.LOBBY);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        GameObject character = GameVar.Ins.Player;

        character.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);

        HealthEngine engine = character.GetComponent<HealthEngine>();
        ((Base)engine.CurrentState).ForceDie();
    }
}
