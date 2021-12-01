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
    List<string> _playerNames = new List<string>();

    bool _initiated = false;
    RoomViewMode _viewMode;

    protected override void Start()
    {
        base.Start();
        this._lbIdRoom = uiHelper.ui[LB_ID_ROOM].GetComponent<Text>();
        this._btnStart = uiHelper.ui[BTN_START].GetComponent<Button>();
        this._btnLeave = uiHelper.ui[BTN_LEAVE].GetComponent<Button>();
        int maxPlayer = LobbyConst.MAX_PLAYER;
        for (int i = 0; i < maxPlayer; i++)
        {
            string uiName = PNL_PLAYER + i.ToString();
            PnlPlayerSlot slot = uiHelper.ui[uiName].GetComponent<PnlPlayerSlot>();
            slot.SetController(this);
            this._playerSlots.Add(slot);
        }

        this._btnLeave.onClick.AddListener(this.OnLeave);
        this._btnStart.onClick.AddListener(this.OnStart);
    }

    public override void OnEnter()
    {
        this._lbIdRoom.text = "ID Room: " + NetworkLobby.Ins.CurrentRoom.Name;
        this._playerNames = new List<string>(){
                "John Wick",
                "John Wick",
                "John Wick",
                "John Wick"
            };
        this.UpdatePlayerSlot();
    }

    void UpdatePlayerSlot()
    {
        for (int i = 0; i < this._playerSlots.Count; i++)
        {
            PnlPlayerSlot slot = this._playerSlots[i];

            if (i >= this._playerNames.Count)
            {
                slot.SetSlotState(SlotState.EMPTY);
            }
            else
            {
                slot.SetSlotState(SlotState.OCCUPIED);
                string name = this._playerNames[i];
                slot.SetName(name);
            }

            if (this._viewMode == RoomViewMode.HOST && slot.GetSlotState() == SlotState.OCCUPIED)
            {
                slot.SetVisibleBtnKick(true);
            }
            else
            {
                slot.SetVisibleBtnKick(false);
            }
        }
    }

    public void AddNewPlayer(string playerName)
    {
        this._playerNames.Add(playerName);
        this.UpdatePlayerSlot();
    }

    public void RemovePlayer(string playerName)
    {
        this._playerNames.Remove(playerName);
        this.UpdatePlayerSlot();
    }

    void OnLeave()
    {
        NetworkLobby.Ins.LeaveRoom();
    }

    void OnStart()
    {
        SceneManager.LoadScene(SceneId.GAME);
    }

    public void SetViewMode(RoomViewMode viewMode)
    {
        this._viewMode = viewMode;
        this.UpdatePlayerSlot();
    }
}

public enum RoomViewMode
{
    HOST,
    GUEST,
}
