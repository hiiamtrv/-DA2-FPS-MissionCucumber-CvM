using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PackListenerLoading : BasePackListener
{
    protected override List<(CMD cmd, Action<object> action)> Listeners =>
        new List<(CMD cmd, Action<object> action)>()
        {
            (CMD.MAKE_TEAM, this.OnMakeTeam)
        };

    void OnMakeTeam(object pack)
    {
        MakeTeamDataPack data = new MakeTeamDataPack();
        data.ReadData(pack as EventData);

        NetworkLoading.Ins.StartGame(data.StartTime);
    }
}
