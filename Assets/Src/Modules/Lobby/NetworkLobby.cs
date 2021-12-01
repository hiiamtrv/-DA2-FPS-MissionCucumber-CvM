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
    const int MAX_LENGTH_ROOM_NAME = 2;

    static HashSet<int> _roomIdexes;
    static NetworkLobby _ins;
    public static NetworkLobby Ins => _ins;

    int _thisRoomIdx;

    RoomInfo _curRoom;
    public RoomInfo CurrentRoom => this._curRoom;

    void Start()
    {
        base.Start();

        _ins = this;
        _roomIdexes = new HashSet<int>();
        this._thisRoomIdx = 0;

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Get List room:");
        _roomIdexes.Clear();
        roomList.ForEach(room =>
        {
            Debug.LogFormat("Room ", room.ToStringFull());
            int roomIdx = Int32.Parse(room.Name);
            _roomIdexes.Add(roomIdx);
        });
        base.OnRoomListUpdate(roomList);
    }

    public void CreateRoom()
    {
        string roomIdx = this.GetEmptyRoomIndex();
        if (roomIdx != "null")
        {
            Debug.LogFormat("[Lobby] attemp create room {0}", roomIdx);
            PhotonNetwork.CreateRoom(roomIdx);
        }
        else
        {
            Debug.LogFormat("[Lobby] cannot find room, room empty is null");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //try create another room
        Debug.LogFormat("[Lobby] room create failed, message: {0}", message);
        this.CreateRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.LogFormat("[Lobby] room create success");
        this._curRoom = PhotonNetwork.CurrentRoom;
        CanvasRoom room = GuiMgr.GetGui(Gui.ROOM).GetComponent<CanvasRoom>();
        room.SetViewMode(RoomViewMode.HOST);
        Gm.ChangeGui(Gui.ROOM);
    }

    string GetEmptyRoomIndex()
    {
        if (_roomIdexes.Count >= MAX_NUM_ROOM) return "null";
        while (_roomIdexes.Contains(this._thisRoomIdx))
        {
            this._thisRoomIdx = (this._thisRoomIdx + 1) % MAX_NUM_ROOM;
        }
        string roomIdx = _thisRoomIdx.ToString();
        return roomIdx.PadLeft(MAX_LENGTH_ROOM_NAME, '0');
    }

    public void LeaveRoom()
    {
        Debug.LogFormat("[Lobby] attempt leave room");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        this._curRoom = null;
        Debug.LogFormat("[Lobby] room left success");
        Gm.ChangeGui(Gui.MAIN_MENU);
    }
}
