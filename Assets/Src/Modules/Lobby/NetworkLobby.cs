using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class NetworkLobby : BaseNetwork
{
    //room id: 00 -> 99
    const int MAX_NUM_ROOM = 100;
    public const int MAX_LENGTH_ROOM_NAME = 2;

    static HashSet<int> _roomIdexes;
    static NetworkLobby _ins;
    public static NetworkLobby Ins => _ins;

    int _thisRoomIdx;

    RoomInfo _curRoom;
    public RoomInfo CurrentRoom => this._curRoom;

    protected override void Awake()
    {
        base.Awake();

        _ins = this;
        _roomIdexes = new HashSet<int>();
        this._thisRoomIdx = 0;

        PhotonNetwork.AutomaticallySyncScene = true;
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

    #region CREATE ROOM
    public void CreateRoom()
    {
        string roomIdx = this.GetEmptyRoomIndex();
        if (roomIdx != "null")
        {
            UnityEngine.Debug.LogFormat("[Lobby] attemp create room {0}", roomIdx);
            PhotonNetwork.CreateRoom(roomIdx);
        }
        else
        {
            UnityEngine.Debug.LogFormat("[Lobby] cannot find room, room empty is null");
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
        Debug.Log("[Lobby] room create success", PhotonNetwork.CurrentRoom);
        this._curRoom = PhotonNetwork.CurrentRoom;
        CanvasRoom room = GuiMgr.GetGui(Gui.ROOM).GetComponent<CanvasRoom>();
        room.SetViewMode(RoomViewMode.HOST);
        Gm.ChangeGui(Gui.ROOM);
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
        this._curRoom = PhotonNetwork.CurrentRoom;
        CanvasRoom room = GuiMgr.GetGui(Gui.ROOM).GetComponent<CanvasRoom>();
        room.SetViewMode(RoomViewMode.GUEST);
        Gm.ChangeGui(Gui.ROOM);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }
    #endregion

    #region LEAVE ROOM
    public void LeaveRoom()
    {
        UnityEngine.Debug.LogFormat("[Lobby] attempt leave room");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        this._curRoom = null;
        UnityEngine.Debug.LogFormat("[Lobby] room left success");
        Gm.ChangeGui(Gui.MAIN_MENU);
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }
    #endregion
}
