using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackListenerLobby : BasePackListener
{
    protected override List<(CMD cmd, Action<object> action)> Listeners =>
        new List<(CMD cmd, Action<object> action)>()
        {
            (CMD.START_GAME, (pack) => NetworkLobby.Ins.OnStartGame())
        };
}
