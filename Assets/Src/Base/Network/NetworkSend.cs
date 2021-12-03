using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

partial class Network
{
    public static void Send(CMD cmd)
    {
        Send(cmd, null);
    }

    public static void Send(CMD cmd, params object[] content)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        Send(cmd, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void Send(CMD cmd, object[] content, ReceiverGroup receiverGroup)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = receiverGroup };
        Send(cmd, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void Send(CMD cmd, object[] content, SendOptions sendOptions)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        Send(cmd, content, raiseEventOptions, sendOptions);
    }

    public static void Send(CMD cmd, object[] content, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
    {
        byte cmdId = (byte)cmd;
        Debug.Log("[Network] send packet", cmdId, content, raiseEventOptions, sendOptions);
        EventCenter.Publish(EventId.NETWORK_SEND, cmdId);
        PhotonNetwork.RaiseEvent(cmdId, content, raiseEventOptions, sendOptions);
    }
}
