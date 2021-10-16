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

    CanvasRoom _controller = null;

    // Start is called before the first frame update
    void Start()
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
        this.SetEnabledRectTransform(this._pnlEmpty, newState == SlotState.EMPTY);
        this.SetEnabledRectTransform(this._pnlOccupied, newState == SlotState.OCCUPIED);
    }

    public void EmptySlot()
    {
        this.SetName("");
        this.SetSlotState(SlotState.EMPTY);
    }

    public void OccupySlot(string username)
    {
        this.SetName(username);
        this.SetSlotState(SlotState.OCCUPIED);
    }

    void SetEnabledRectTransform(RectTransform panel, bool enabled)
    {
        panel.gameObject.SetActive(enabled);
    }

    void KickPlayer()
    {
        string playerName = this.GetName();
        this._controller.RemovePlayer(playerName);
    }

    public void SetController(CanvasRoom controller)
    {
        this._controller = controller;
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
