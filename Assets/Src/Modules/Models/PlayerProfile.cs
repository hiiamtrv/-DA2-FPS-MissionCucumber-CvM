public class PlayerProfile
{
    string _userName;
    string _description;

    int _numKill;
    int _numDeath;
    int _numAssist;

    int _curRankPoint;
    int _highestRankPoint;

    public PlayerProfile(string userName)
    {
        this._userName = userName;
        this._description = "";
        this._numKill = 0;
        this._numDeath = 0;
        this._numAssist = 0;
        this._curRankPoint = 0;
        this._highestRankPoint = 0;
    }

    string GetUserName() { return this._userName; }

    public string GetDescription() { return this._description; }
    public void SetDescription(string newDescription)
    {
        this._description = newDescription;
        EventCenter.Publish(EventId.PLAYER_PROFILE_CHANGE);
    }

    public int GetNumKill() { return this._numKill; }
    public void SetNumKill(int newNumKill)
    {
        this._numKill = newNumKill;
        EventCenter.Publish(EventId.PLAYER_PROFILE_CHANGE);
    }

    public int GetNumDeath() { return this._numDeath; }
    public void SetNumDeath(int newNumDeath)
    {
        this._numDeath = newNumDeath;
        EventCenter.Publish(EventId.PLAYER_PROFILE_CHANGE);
    }

    public int GetNumAssist() { return this._numAssist; }
    public void SetNumAssist(int newNumAssist)
    {
        this._numAssist = newNumAssist;
        EventCenter.Publish(EventId.PLAYER_PROFILE_CHANGE);
    }

    public int GetCurrentRankPoint() { return this._curRankPoint; }
    public void SetCurrentRankPoint(int newRankPoint)
    {
        this._curRankPoint = newRankPoint;
        EventCenter.Publish(EventId.PLAYER_PROFILE_CHANGE);
    }

    public int GetHighestRankPoint() { return this._highestRankPoint; }
    public void SetHighestRankPoint(int newHighestRankPoint)
    {
        this._highestRankPoint = newHighestRankPoint;
        EventCenter.Publish(EventId.PLAYER_PROFILE_CHANGE);
    }
}