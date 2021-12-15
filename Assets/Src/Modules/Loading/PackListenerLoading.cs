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

        GameVar.Players = data.TeamResult;
        GameVar.MySpawnIndex = data.SpawnIndex[NetworkLoading.Ins.MyUserId];
        GameVar.StartSide = (CharacterSide)data.TeamResult[NetworkLoading.Ins.MyUserId];
        GameVar.CucumberIndex = data.CucumberIndex;
        NetworkLoading.Ins.StartGame(data.StartTime);
    }
}
