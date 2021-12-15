using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class MakeTeamDataPack : BaseDataPack
{
    Dictionary<string, int> _teamResult;
    public Dictionary<string, int> TeamResult => _teamResult;

    Dictionary<string, int> _spawnIndex;
    public Dictionary<string, int> SpawnIndex => _spawnIndex;

    long _startTime;
    public long StartTime => _startTime;

    List<int> _cucumberIndex;
    public List<int> CucumberIndex => _cucumberIndex;

    public MakeTeamDataPack() : base() { }

    public MakeTeamDataPack(
        Dictionary<string, int> teamResult,
        Dictionary<string, int> spawnIndex,
        long startTime,
        List<int> cucumberIndex
        ) : base()
    {
        this._teamResult = teamResult;
        this._spawnIndex = spawnIndex;
        this._startTime = startTime;
        this._cucumberIndex = cucumberIndex;
    }

    public override void WriteData()
    {
        this.PutValue(this._teamResult);
        this.PutValue(this._spawnIndex);
        this.PutValue(this._startTime);
        this.PutValue(this._cucumberIndex);
    }

    public override void ReadData(EventData eventData)
    {
        base.ReadData(eventData);
        this._teamResult = this.GetNextValue<Dictionary<string, int>>();
        this._spawnIndex = this.GetNextValue<Dictionary<string, int>>();
        this._startTime = this.GetNextValue<long>();
        this._cucumberIndex = this.GetNextValue<List<int>>();
    }
}
