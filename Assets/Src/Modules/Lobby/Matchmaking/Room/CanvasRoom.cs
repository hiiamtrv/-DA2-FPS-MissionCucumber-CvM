using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasRoom : BaseGui
{
    const string BTN_START = "BtnStart";
    const string PNL_PLAYER = "PnlPlayer";
    const string BTN_LEAVE = "BtnLeave";
    const string LB_ID_ROOM = "LbIdRoom";

    Button _btnStart = null;
    Button _btnLeave = null;
    Text _lbIdRoom = null;
    List<PnlPlayerSlot> _playerSlots = new List<PnlPlayerSlot>();
    List<PlayerDisplayData> _playerData = new List<PlayerDisplayData>();

    bool _initiated = false;
    RoomViewMode _viewMode;

    protected override void Awake()
    {
        base.Awake();

        this._lbIdRoom = uiHelper.ui[LB_ID_ROOM].GetComponent<Text>();
        this._btnStart = uiHelper.ui[BTN_START].GetComponent<Button>();
        this._btnLeave = uiHelper.ui[BTN_LEAVE].GetComponent<Button>();

        this._btnLeave.onClick.AddListener(this.RequestLeave);
        this._btnStart.onClick.AddListener(this.RequestStart);

        EventCenter.Subcribe(EventId.PLAYER_JOIN_ROOM, this.AddNewPlayer);
        EventCenter.Subcribe(EventId.PLAYER_LEAVE_ROOM, this.RemovePlayer);
        EventCenter.Subcribe(EventId.REMOVE_PLAYER_IN_ROOM, this.RemoveAllPlayer);
        EventCenter.Subcribe(EventId.PLAYER_IS_HOST, (data) => this.SetViewMode(RoomViewMode.HOST));

        this._playerSlots.Clear();
        this._playerData.Clear();
    }

    void ImportPlayerSlot()
    {
        int maxPlayer = NetworkLobby.MAX_PLAYER_IN_ROOM;
        this._playerSlots.Clear();
        for (int i = 0; i < maxPlayer; i++)
        {
            string uiName = PNL_PLAYER + i.ToString();
            PnlPlayerSlot slot = uiHelper.ui[uiName].GetComponent<PnlPlayerSlot>();
            slot.RoomViewMode = this._viewMode;
            slot.IsHostSlot = (i == 0);
            this._playerSlots.Add(slot);
        }
    }

    public override void OnEnter()
    {
        this._lbIdRoom.text = "ID Room: " + NetworkLobby.Ins.MyRoomInfo.Name;
    }

    public override void OnExit()
    {
        this._playerSlots.Clear();
    }

    void UpdatePlayerSlot()
    {
        this.ImportPlayerSlot();
        this._playerSlots.ForEach(slot => slot.EmptySlot());

        this._playerData.Sort((a, b) =>
        {
            int hostA = a.IsHost ? 1 : 0;
            int hostB = b.IsHost ? 1 : 0;
            return hostB - hostA;
        });

        for (var i = 0; i < this._playerData.Count; i++)
        {
            PnlPlayerSlot slot = this._playerSlots[i];
            PlayerDisplayData data = this._playerData[i];
            slot.OccupySlot(data);
        }
    }

    void AddNewPlayer(object pubData)
    {
        PlayerDisplayData player = (PlayerDisplayData)pubData;
        if (!this._playerData.Contains(player))
        {
            this._playerData.Add(player);
            this.UpdatePlayerSlot();
        }
    }

    void RemovePlayer(object pubData)
    {
        string removePlayerId = pubData as string;
        this._playerData = this._playerData.FindAll(player => player.UserId != removePlayerId);
        this.UpdatePlayerSlot();
    }

    void RemoveAllPlayer(object pubData)
    {
        this._playerData.Clear();
        this.UpdatePlayerSlot();
    }

    void RequestLeave()
    {
        NetworkLobby.Ins.LeaveRoom();
    }

    void RequestStart()
    {
        NetworkLobby.Ins.StartGame();
    }

    public void SetViewMode(RoomViewMode viewMode)
    {
        this._viewMode = viewMode;
        this.UpdatePlayerSlot();
        this._btnStart.enabled = viewMode == RoomViewMode.HOST;
    }
}

public enum RoomViewMode
{
    HOST,
    GUEST,
}
