using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlPlayerSlot : MonoBehaviour
{
    const string PNL_EMPTY = "PnlEmpty";
    const string PNL_OCCUPIED = "PnlOccupied";
    const string LB_PLAYER_NAME = "LbPlayerName";
    const string BTN_KICK = "BtnKick";

    const string DEFAULT_NAME = "#DefaultName";

    UiHelper uiHelper = null;
    RectTransform _pnlEmpty = null;
    RectTransform _pnlOccupied = null;
    Text _lbPlayerName = null;
    Button _btnKick = null;
    SlotState _curSlotState;

    PlayerDisplayData _playerData = null;

    bool _isHostSlot = false;
    public bool IsHostSlot { get => _isHostSlot; set => _isHostSlot = value; }

    RoomViewMode _roomViewMode;
    public RoomViewMode RoomViewMode { get => _roomViewMode; set => _roomViewMode = value; }

    // Start is called before the first frame update
    void Awake()
    {
        this.uiHelper = new UiHelper(this.gameObject);

        this._pnlEmpty = this.uiHelper.ui[PNL_EMPTY].GetComponent<RectTransform>();
        this._pnlOccupied = this.uiHelper.ui[PNL_OCCUPIED].GetComponent<RectTransform>();

        this._lbPlayerName = uiHelper.ui[LB_PLAYER_NAME].GetComponent<Text>();
        this._btnKick = uiHelper.ui[BTN_KICK].GetComponent<Button>();

        this._btnKick.onClick.AddListener(this.KickPlayer);

        this.SetName(DEFAULT_NAME);
        this.SetSlotState(SlotState.OCCUPIED);
    }

    public string GetName()
    {
        return this._lbPlayerName.text;
    }

    public void SetName(string name)
    {
        this._lbPlayerName.text = name;
    }

    public SlotState GetSlotState()
    {
        return this._curSlotState;
    }

    public void SetSlotState(SlotState newState)
    {
        this._curSlotState = newState;
        this._pnlEmpty.gameObject.SetActive(newState == SlotState.EMPTY);
        this._pnlOccupied.gameObject.SetActive(newState == SlotState.OCCUPIED);
    }

    public void EmptySlot()
    {
        this._playerData = null;
        this.SetName("");
        this.SetSlotState(SlotState.EMPTY);
        this._btnKick.gameObject.SetActive(false);
    }

    public void OccupySlot(PlayerDisplayData playerData)
    {
        this._playerData = playerData;
        this.SetName(playerData.UserName);
        this.SetSlotState(SlotState.OCCUPIED);

        bool enableKictButton = this._roomViewMode == RoomViewMode.HOST && !this._isHostSlot;
        this._btnKick.gameObject.SetActive(enableKictButton);
    }

    void SetEnabledRectTransform(RectTransform panel, bool enabled)
    {
        panel.gameObject.SetActive(enabled);
    }

    void KickPlayer()
    {
        NetworkLobby.Ins.KickPlayer(this._playerData.UserId);
    }

    public void SetVisibleBtnKick(bool enable)
    {
        this._btnKick.gameObject.SetActive(enable);
    }
}

public enum SlotState
{
    EMPTY,
    OCCUPIED,
}
