using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class MakeTeamDataPack : BaseDataPack
{
    Dictionary<string, int> _teamResult;
    public Dictionary<string, int> TeamResult => _teamResult;

    long _startTime;
    public long StartTime => _startTime;

    public MakeTeamDataPack() : base() { }

    public MakeTeamDataPack(Dictionary<string, int> teamResult, long startTime) : base()
    {
        this._teamResult = teamResult;
        this._startTime = startTime;
    }

    public override void WriteData()
    {
        this.PutValue(this._teamResult);
        this.PutValue(this._startTime);
    }

    public override void ReadData(EventData eventData)
    {
        base.ReadData(eventData);
        this._teamResult = this.GetNextValue<Dictionary<string, int>>();
        this._startTime = this.GetNextValue<long>();
    }
}
