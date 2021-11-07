using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

partial class Network
{
    static List<NetworkListener> _listeners = new List<NetworkListener>();

    public static void Receive(EventData photonEvent)
    {
        byte cmd = photonEvent.Code;
        List<NetworkListener> suitableListeners = _listeners.FindAll(listener => listener.Cmd == cmd);
        suitableListeners.ForEach(listener =>
        {
            listener.Action(photonEvent);
        });
    }

    public static void AssignListener(byte cmd, Action<object> action)
    {
        if (_listeners.Find(listener => listener.Cmd == cmd && listener.Action == action) != null) return;

        NetworkListener listener = new NetworkListener(cmd, action);
        _listeners.Add(listener);
    }

    public static void RemoveListener(byte cmd, Action<object> action)
    {
        _listeners.RemoveAll(listener => listener.Cmd == cmd && listener.Action == action);
    }

    #region NETWORK_LISTENER
    class NetworkListener
    {
        byte _cmd;
        Action<object> _action;

        public NetworkListener(byte cmd, Action<object> action)
        {
            this._cmd = cmd;
            this._action = action;
        }

        public bool IsValid => (this._action != null);
        public byte Cmd => this._cmd;
        public Action<object> Action => this._action;
    }
    #endregion
}
