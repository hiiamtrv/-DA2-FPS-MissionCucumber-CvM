using UnityEngine;

public class PlayerProfile
{
    string _username;
    string _description;

    int _numKill;
    int _numDeath;
    int _numAssist;

    int _curRankPoint;
    int _highestRankPoint;

    public bool HasLocalData()
    {
        bool hasUsername = PlayerPrefs.HasKey(LocalKey.PLAYER_PROFILE_USERNAME);
        return (hasUsername);
    }

    public PlayerProfile()
    {
        if (this.HasLocalData())
        {
            Debug.Log("[PlayerProfile] has local data");
            this._username = PlayerPrefs.GetString(LocalKey.PLAYER_PROFILE_USERNAME);
            this._description = PlayerPrefs.GetString(LocalKey.PLAYER_PROFILE_DESCRIPTION);
            this._numKill = PlayerPrefs.GetInt(LocalKey.PLAYER_PROFILE_NUMKILL);
            this._numDeath = PlayerPrefs.GetInt(LocalKey.PLAYER_PROFILE_NUMDEATH);
            this._numAssist = PlayerPrefs.GetInt(LocalKey.PLAYER_PROFILE_NUMASSIST);
            this._curRankPoint = PlayerPrefs.GetInt(LocalKey.PLAYER_PROFILE_CURRANKPOINT);
            this._highestRankPoint = PlayerPrefs.GetInt(LocalKey.PLAYER_PROFILE_HIGHESTRANKPOINT);
        }
        else
        {
            Debug.Log("[PlayerProfile] has no local data");
            this._username = "";
            this._description = "";
            this._numKill = 0;
            this._numDeath = 0;
            this._numAssist = 0;
            this._curRankPoint = 0;
            this._highestRankPoint = 0;
        }
        this.LogPlayerProfile();
    }

    public string GetUsername() { return this._username; }
    public void SetUsername(string newUsername, bool saveToLocal = true)
    {
        this._username = newUsername.Trim();
        this._description = "";
        this._numKill = 0;
        this._numDeath = 0;
        this._numAssist = 0;
        this._curRankPoint = 0;
        this._highestRankPoint = 0;
        this.UpdatePlayerProfile(saveToLocal);
    }

    public string GetDescription() { return this._description; }
    public void SetDescription(string newDescription, bool saveToLocal = true)
    {
        this._description = newDescription.Trim();
        this.UpdatePlayerProfile(saveToLocal);
    }

    public int GetNumKill() { return this._numKill; }
    public void SetNumKill(int newNumKill, bool saveToLocal = true)
    {
        this._numKill = newNumKill;
        this.UpdatePlayerProfile(saveToLocal);
    }

    public int GetNumDeath() { return this._numDeath; }
    public void SetNumDeath(int newNumDeath, bool saveToLocal = true)
    {
        this._numDeath = newNumDeath;
        this.UpdatePlayerProfile(saveToLocal);
    }

    public int GetNumAssist() { return this._numAssist; }
    public void SetNumAssist(int newNumAssist, bool saveToLocal = true)
    {
        this._numAssist = newNumAssist;
        this.UpdatePlayerProfile(saveToLocal);
    }

    public int GetCurrentRankPoint() { return this._curRankPoint; }
    public void SetCurrentRankPoint(int newRankPoint, bool saveToLocal = true)
    {
        this._curRankPoint = newRankPoint;
        this.UpdatePlayerProfile(saveToLocal);
    }

    public int GetHighestRankPoint() { return this._highestRankPoint; }
    public void SetHighestRankPoint(int newHighestRankPoint, bool saveToLocal = true)
    {
        this._highestRankPoint = newHighestRankPoint;
        this.UpdatePlayerProfile(saveToLocal);
    }

    void UpdatePlayerProfile(bool saveToLocal)
    {
        EventCenter.Publish(EventId.PLAYER_PROFILE_CHANGE);
        if (saveToLocal) this.SaveOnLocalData();
        this.LogPlayerProfile();
    }

    void LogPlayerProfile()
    {
        Debug.Log("[PlayerProfile] log player profile");
        Debug.Log("username:\t" + this._username);
        Debug.Log("description:\t" + this._description);
        Debug.Log("numKill:\t" + this._numKill);
        Debug.Log("numDeath:\t" + this._numDeath);
        Debug.Log("numAssist:\t" + this._numAssist);
        Debug.Log("curRP:\t" + this._curRankPoint);
        Debug.Log("highestRP:\t" + this._highestRankPoint);
    }

    void SaveOnLocalData()
    {
        PlayerPrefs.SetString(LocalKey.PLAYER_PROFILE_USERNAME, this._username);
        PlayerPrefs.SetString(LocalKey.PLAYER_PROFILE_DESCRIPTION, this._description);
        PlayerPrefs.SetInt(LocalKey.PLAYER_PROFILE_NUMKILL, this._numKill);
        PlayerPrefs.SetInt(LocalKey.PLAYER_PROFILE_NUMDEATH, this._numDeath);
        PlayerPrefs.SetInt(LocalKey.PLAYER_PROFILE_NUMASSIST, this._numAssist);
        PlayerPrefs.SetInt(LocalKey.PLAYER_PROFILE_CURRANKPOINT, this._curRankPoint);
        PlayerPrefs.SetInt(LocalKey.PLAYER_PROFILE_HIGHESTRANKPOINT, this._highestRankPoint);
    }
}