using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class MakeTeamDataPack : BaseDataPack
{
    TeamMaker.Result _teamResult;
    public TeamMaker.Result TeamResult => _teamResult;

    float _startTime;
    public float StartTime => _startTime;

    public MakeTeamDataPack() : base() { }

    public MakeTeamDataPack(TeamMaker.Result teamResult, float startTime) : base()
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
        this._teamResult = this.GetNextValue<TeamMaker.Result>();
        this._startTime = this.GetNextValue<float>();
    }
}
