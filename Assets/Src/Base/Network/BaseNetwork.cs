using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BaseNetwork : MonoBehaviourPunCallbacks
{
    public string MyUserId => PhotonNetwork.AuthValues.UserId;
    public string MyUserName => PhotonNetwork.NickName;

    private static bool _connected = false;

    protected virtual void Awake()
    {
        if (!BaseNetwork._connected)
        {
            this.InitNetwork();
        }
    }

    void InitNetwork()
    {
        Debug.Log("Initializing connection to server...");
        Debug.Log("Current version:", GameVersion.CURRENT_VERSION);
        Debug.Log("Server settings: ", PhotonNetwork.PhotonServerSettings.ToString());

        PhotonNetwork.GameVersion = GameVersion.CURRENT_VERSION;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AuthValues = new Photon.Realtime.AuthenticationValues();
    }

    public override void OnConnectedToMaster()
    {
        _connected = true;
        EventCenter.Publish(EventId.NETWORK_CONNECTED);
        UnityEngine.Debug.Log("Client has connected");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _connected = false;
        EventCenter.Publish(EventId.NETWORD_DISCONNECT, cause);
        UnityEngine.Debug.Log("Client has disconnected - Reason: " + cause.ToString());
    }

    public void LogInfo()
    {
        UnityEngine.Debug.Log("[BaseNetwork] Logging infomation ...");
        UnityEngine.Debug.Log("Connected: " + PhotonNetwork.IsConnected);
        UnityEngine.Debug.Log("Server: " + PhotonNetwork.ServerAddress);
        UnityEngine.Debug.Log("Nickname: " + PhotonNetwork.NickName);
        UnityEngine.Debug.Log("Uid: " + PhotonNetwork.AuthValues.UserId);
    }
}
