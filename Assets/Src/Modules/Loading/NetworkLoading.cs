using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;
using BayatGames.Serialization.Formatters.Json;

public class NetworkLoading : BaseNetwork
{
    const float DELAY_BEFORE_START = 3;

    static NetworkLoading _ins;
    public static NetworkLoading Ins => _ins;

    protected override void Awake()
    {
        base.Awake();
        _ins = this;
        PhotonNetwork.EnableCloseConnection = false;

        if (PhotonNetwork.IsMasterClient)
        {
            this.GenerateTeams();
        }
    }

    public void StartGame(float startTime)
    {
        float waitSecond = startTime - Time.fixedTime;
        LeanTween.delayedCall(waitSecond, () =>
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneId.GAME)
            {
                SceneManager.LoadScene(SceneId.GAME);
            }
        });
    }

    void GenerateTeams()
    {
        List<Player> listPlayer = new List<Player>(this.MyRoom.Players.Values);
        List<string> listUserId = listPlayer.ConvertAll(player => player.UserId);

        TeamMaker.Result resultTeam = TeamMaker.GenerateTeams(listUserId);
        float startTime = Time.fixedTime + DELAY_BEFORE_START;

        MakeTeamDataPack packData = new MakeTeamDataPack(resultTeam, startTime);
        packData.WriteData();
        Network.Send(CMD.MAKE_TEAM, packData.ForSend);
    }
}
