using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRoom : MonoBehaviour
{
    const string BTN_START = "BtnStart";
    const string PNL_PLAYER = "PnlPlayer";
    const string BTN_LEAVE = "BtnLeave";

    UiHelper uiHelper = null;
    Button _btnStart = null;
    Button _btnLeave = null;
    List<PnlPlayerSlot> _playerSlots = new List<PnlPlayerSlot>();
    List<string> _playerNames = new List<string>();

    bool _initiated = false;
    RoomViewMode _viewMode;

    // Start is called before the first frame update
    void Start()
    {
        this.uiHelper = new UiHelper(this.gameObject);
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
    }

    void Update()
    {
        if (!this._initiated)
        {
            this._playerNames = new List<string>(){
                "John Wick",
                "John Wick",
                "John Wick",
                "John Wick"
            };
            this.UpdatePlayerSlot();
            this._initiated = true;
        }
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
        Gm.ChangeGui(Gui.MAIN_MENU);
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
