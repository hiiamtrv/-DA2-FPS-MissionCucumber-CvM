using Photon.Realtime;

public class PlayerDisplayData
{
    string _userId;
    string _userName;
    bool _isHost;

    public string UserId => _userId;
    public string UserName => _userName;
    public bool IsHost => _isHost;

    public PlayerDisplayData(string userId, string userName, bool isHost)
    {
        this._userId = userId;
        this._userName = userName;
        this._isHost = isHost;
    }

    public PlayerDisplayData(Player player)
    {
        this._userId = player.UserId;
        this._userName = player.NickName;
        this._isHost = player.IsMasterClient;
    }
}