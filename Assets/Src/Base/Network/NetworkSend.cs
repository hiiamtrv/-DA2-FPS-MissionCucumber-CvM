using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

partial class Network
{
    public static void Send(CMD cmd, object[] content)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        Send(cmd, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void Send(CMD cmd, object[] content, ReceiverGroup receiverGroup)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = receiverGroup };
        Send(cmd, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void Send(CMD cmd, object[] content, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
    {
        byte cmdId = (byte)cmd;
        PhotonNetwork.RaiseEvent(cmdId, content, raiseEventOptions, sendOptions);
    }
}
