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
    const long DELAY_BEFORE_START = 7;

    static NetworkLoading _ins;
    public static NetworkLoading Ins => _ins;

    protected override void Awake()
    {
        base.Awake();
        _ins = this;
        PhotonNetwork.EnableCloseConnection = false;

        if (PhotonNetwork.IsMasterClient)
        {
            this.PrepareMatch();
        }
    }

    public void StartGame(long startTime)
    {
    EventCenter.Renew();
        Time.timeScale = 0.5f;
        SceneManager.LoadScene(SceneId.CUTSCENE);

        long waitSecond = startTime - TimeUtils.Now();
        LeanTween.delayedCall(waitSecond, () =>
        {
            Debug.Log("Game starts in", waitSecond, "seconds");
            if (SceneManager.GetActiveScene().buildIndex != SceneId.GAMEDEMO)
            {
                Time.timeScale = 1;
                int sceneId = SceneId.GAMEDEMO;
                EventCenter.Renew();
                SceneManager.LoadScene(sceneId);
            }
        });
    }

    void PrepareMatch()
    {
        List<Player> listPlayer = new List<Player>(this.MyRoom.Players.Values);
        List<string> listUserId = listPlayer.ConvertAll(player => player.UserId);

        Dictionary<string, int> resultTeam = TeamMaker.GenerateTeams(listUserId);
        Dictionary<string, int> spawnIndex = TeamMaker.GetSpawnIndex(resultTeam);
        long startTime = TimeUtils.Now() + DELAY_BEFORE_START;
        List<int> cucumberIndex = CucumberSpawn.GetSpawnIndexes();

        MakeTeamDataPack packData = new MakeTeamDataPack(resultTeam, spawnIndex, startTime, cucumberIndex);
        packData.WriteData();
        Network.Send(CMD.PREPARE_MATCH, packData.ForSend);
    }
}
