using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class NetworkLobby : BaseNetwork
{
    //room id: 00 -> 99
    const int MAX_NUM_ROOM = 100;
    public const int MAX_LENGTH_ROOM_NAME = 2;
    public const int MAX_PLAYER_IN_ROOM = 6;

    static HashSet<int> _roomIdexes;
    static NetworkLobby _ins;
    public static NetworkLobby Ins => _ins;

    int _thisRoomIdx;

    protected override void Awake()
    {
        base.Awake();

        _ins = this;
        _roomIdexes = new HashSet<int>();
        this._thisRoomIdx = 0;

        PhotonNetwork.EnableCloseConnection = true;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        UnityEngine.Debug.Log("Get List room:");
        _roomIdexes.Clear();
        roomList.ForEach(room =>
        {
            Debug.Log("Room ", room.ToStringFull());
            int roomIdx = Int32.Parse(room.Name);
            _roomIdexes.Add(roomIdx);
        });
        base.OnRoomListUpdate(roomList);
    }

    void AchieveRoomPlayerData()
    {
        EventCenter.Publish(EventId.REMOVE_PLAYER_IN_ROOM);
        Dictionary<int, Player> players = this.MyRoom.Players;
        foreach (var item in players)
        {
            EventCenter.Publish(EventId.PLAYER_JOIN_ROOM, new PlayerDisplayData(item.Value));
        }
    }

    #region CREATE ROOM
    public void CreateRoom()
    {
        string roomIdx = this.GetEmptyRoomIndex();
        if (roomIdx != "null")
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = NetworkLobby.MAX_PLAYER_IN_ROOM;
            options.PublishUserId = true;

            UnityEngine.Debug.LogFormat("[Lobby] attemp create room {0}", roomIdx);
            PhotonNetwork.CreateRoom(roomIdx, options);
        }
        else
        {
            UnityEngine.Debug.LogFormat("[Lobby] cannot create a room: no more empty room");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        //try create another room
        UnityEngine.Debug.LogFormat("[Lobby] room create failed, message: {0}", message);
        this.CreateRoom();
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("[Lobby] room create success", this.MyRoom);

        CanvasRoom room = GuiMgr.GetGui(Gui.ROOM).GetComponent<CanvasRoom>();
        room.SetViewMode(RoomViewMode.HOST);
        Gm.ChangeGui(Gui.ROOM);

        this.AchieveRoomPlayerData();

        PhotonNetwork.LeaveLobby();
    }

    string GetEmptyRoomIndex()
    {
        if (_roomIdexes.Count >= MAX_NUM_ROOM) return "null";

        this._thisRoomIdx = MathUtils.RandomInt(0, MAX_NUM_ROOM);
        while (_roomIdexes.Contains(this._thisRoomIdx))
        {
            this._thisRoomIdx = (this._thisRoomIdx + 1) % MAX_NUM_ROOM;
        }
        string roomIdx = _thisRoomIdx.ToString();
        return roomIdx.PadLeft(MAX_LENGTH_ROOM_NAME, '0');
    }
    #endregion

    #region JOIN ROOM
    public void JoinRoom(string roomCode)
    {
        PhotonNetwork.JoinRoom(roomCode);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        CanvasRoom room = GuiMgr.GetGui(Gui.ROOM).GetComponent<CanvasRoom>();
        bool isHost = PhotonNetwork.IsMasterClient;
        room.SetViewMode(isHost ? RoomViewMode.HOST : RoomViewMode.GUEST);
        Gm.ChangeGui(Gui.ROOM);

        this.AchieveRoomPlayerData();

        PhotonNetwork.LeaveLobby();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        EventCenter.Publish(EventId.PLAYER_JOIN_ROOM, new PlayerDisplayData(newPlayer));
    }
    #endregion

    #region LEAVE ROOM
    public void LeaveRoom()
    {
        Debug.Log("[Lobby] attempt leave room");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("[Lobby] room left success");
        Gm.ChangeGui(Gui.MAIN_MENU);
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("[Lobby] Player left room", otherPlayer.NickName);
        EventCenter.Publish(EventId.PLAYER_LEAVE_ROOM, new PlayerDisplayData(otherPlayer));
    }
    #endregion

    #region SWTICH HOST
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("[Lobby] Switch host");
        base.OnMasterClientSwitched(newMasterClient);
        if (PhotonNetwork.IsMasterClient) EventCenter.Publish(EventId.PLAYER_IS_HOST);
        this.AchieveRoomPlayerData();
    }
    #endregion

    #region KICK_PLAYER
    public void KickPlayer(string playerId)
    {
        Debug.Log("Attempt kick player", playerId);
        if (PhotonNetwork.IsMasterClient)
        {
            Dictionary<int, Player> players = this.MyRoom.Players;
            foreach (var item in players)
            {
                Player player = item.Value;
                if (player.UserId == playerId)
                {
                    Debug.Log("Do kick player", player, playerId, playerId == null);
                    PhotonNetwork.CloseConnection(player);
                    EventCenter.Publish(EventId.PLAYER_LEAVE_ROOM, playerId);
                }
            }
        }
    }
    #endregion

    #region SOLO
    public void GoSolo()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        this.CreateRoom();
    }
    #endregion

    #region START GAME
    public void StartGame()
    {
        Debug.Log("Attemp Start game");
        Network.Send(CMD.START_GAME);
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(SceneId.LOADING);
    }
    #endregion
}
