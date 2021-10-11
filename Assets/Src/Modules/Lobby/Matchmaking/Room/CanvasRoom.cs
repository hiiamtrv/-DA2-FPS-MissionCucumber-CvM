using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRoom : MonoBehaviour
{
    const string BTN_START = "BtnStart";
    const string PNL_PLAYER = "PnlPlayer";
    const string BTN_LEAVE = "BtnLeave";

    Button _btnStart = null;
    Button _btnLeave = null;
    List<PnlPlayerSlot> _playerSlots = new List<PnlPlayerSlot>();
    // int _curNumPlayer;

    // Start is called before the first frame update
    void Start()
    {
        UiHelper uiHelper = new UiHelper(this.gameObject);
        this._btnStart = uiHelper.ui[BTN_START].GetComponent<Button>();
        this._btnLeave = uiHelper.ui[BTN_LEAVE].GetComponent<Button>();
        int maxPlayer = LobbyConst.MAX_PLAYER;
        for (int i = 0; i < maxPlayer; i++)
        {
            string uiName = PNL_PLAYER + i.ToString();
            PnlPlayerSlot newSlot = uiHelper.ui[uiName].GetComponent<PnlPlayerSlot>();
            this._playerSlots.Add(newSlot);
        }
        this._btnLeave.onClick.AddListener(this.OnLeave);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNewPlayer(string playerName)
    {

    }

    public void RemovePlayer(string playerName)
    {

    }

    void OnLeave()
    {
        Gm.ChangeGui(Gui.MAIN_MENU);
    }
}
