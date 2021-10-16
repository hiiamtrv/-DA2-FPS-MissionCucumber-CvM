using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BaseNetwork : MonoBehaviourPunCallbacks
{
    protected static bool IsInitiated = false;

    protected virtual void Start()
    {
        if (!BaseNetwork.IsInitiated)
        {
            this.InitNetwork();
        }
    }

    void InitNetwork()
    {
        Debug.Log("Initializing connection to server...");
        Debug.Log("Server settings: " + PhotonNetwork.PhotonServerSettings.ToString());

        PhotonNetwork.GameVersion = GameVersion.CURRENT_VERSION;
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.AuthValues = new Photon.Realtime.AuthenticationValues();
    }

    public override void OnConnectedToMaster()
    {
        EventCenter.Publish(EventId.NETWORK_CONNECTED);
        Debug.Log("Client has connected");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        EventCenter.Publish(EventId.NETWORD_DISCONNECT, cause);
        Debug.Log("Client has disconnected - Reason: " + cause.ToString());
    }

    public void LogInfo()
    {
        Debug.Log("[BaseNetwork] Logging infomation ...");
        Debug.Log("Connected: " + PhotonNetwork.IsConnected);
        Debug.Log("Server: " + PhotonNetwork.ServerAddress);
        Debug.Log("Nickname: " + PhotonNetwork.NickName);
        Debug.Log("Uid: " + PhotonNetwork.AuthValues.UserId);
    }
}
