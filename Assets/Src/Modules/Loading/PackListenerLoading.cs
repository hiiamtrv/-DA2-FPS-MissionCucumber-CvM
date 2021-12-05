using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using BayatGames.Serialization.Formatters.Json;

public class PackListenerLoading : BasePackListener
{
    protected override List<(CMD cmd, Action<object> action)> Listeners =>
        new List<(CMD cmd, Action<object> action)>()
        {
            (CMD.PREPARE_MATCH, this.OnMakeTeam)
        };

    void OnMakeTeam(object pack)
    {
        MakeTeamDataPack data = new MakeTeamDataPack();
        data.ReadData(pack as EventData);

        Debug.Log("Log data: ", data.ToString(), JsonFormatter.SerializeObject(data.TeamResult));
        GameVar.StartSide = (CharacterSide)data.TeamResult[NetworkLoading.Ins.MyUserId];
        GameVar.CucumberIndex = data.CucumberIndex;
        NetworkLoading.Ins.StartGame(data.StartTime);
    }
}
