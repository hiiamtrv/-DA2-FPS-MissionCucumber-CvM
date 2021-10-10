using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRoom : MonoBehaviour
{
    const string BTN_START = "BtnStart";
    const string PNL_PLAYER = "PnlPlayer";

    Button _btnStart = null;
    List<PnlPlayerSlot> _playerSlots = new List<PnlPlayerSlot>();
    // int _curNumPlayer;

    // Start is called before the first frame update
    void Start()
    {
        UiHelper uiHelper = new UiHelper(this.gameObject);
        this._btnStart = uiHelper.ui[BTN_START].GetComponent<Button>();
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
        EventCenter.Publish(EventId.CHANGE_GUI, Gui.MAKE_ROOM);
    }
}
