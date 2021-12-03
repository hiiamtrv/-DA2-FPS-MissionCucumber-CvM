using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class BasePackListener : MonoBehaviour, IOnEventCallback
{
    protected virtual List<(CMD cmd, Action<object> action)> Listeners =>
        new List<(CMD cmd, Action<object> action)>()
        {
            
        };

    #region DO NOT OVERRIDE
    static BasePackListener _curListener = null;

    void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        if (_curListener != null) PhotonNetwork.RemoveCallbackTarget(_curListener);
        _curListener = this;
        this.Listeners.ForEach(listener =>
        {
            Network.AssignListener((byte)listener.cmd, listener.action);
        });
    }

    void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        _curListener = null;
        this.Listeners.ForEach(listener =>
        {
            Network.RemoveListener((byte)listener.cmd, listener.action);
        });
    }

    public void OnEvent(EventData data)
    {
        Network.Receive(data);
    }
    #endregion
}
