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

    RectTransform _pnlEmpty = null;
    RectTransform _pnlOccupied = null;
    Text _lbPlayerName = null;
    Button _btnKick = null;
    SlotState _curSlotState;

    // Start is called before the first frame update
    void Start()
    {
        UiHelper uiHelper = new UiHelper(this.gameObject);
        
        //at 2 lines below, they both worked but still threw exceptions when hide
        this._pnlEmpty = uiHelper.ui[PNL_EMPTY].GetComponent<RectTransform>();
        this._pnlOccupied = uiHelper.ui[PNL_OCCUPIED].GetComponent<RectTransform>();
        //gonna research this later

        this._lbPlayerName = uiHelper.ui[LB_PLAYER_NAME].GetComponent<Text>();
        this._btnKick = uiHelper.ui[BTN_KICK].GetComponent<Button>();

        this.SetName(DEFAULT_NAME);
        this.SetSlotState(SlotState.EMPTY);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetName()
    {
        return this._lbPlayerName.text;
    }

    void SetName(string name)
    {
        this._lbPlayerName.text = name;
    }

    public SlotState GetSlotState()
    {
        return this._curSlotState;
    }

    void SetSlotState(SlotState newState)
    {
        this._curSlotState = newState;
        switch (newState)
        {
            case SlotState.EMPTY:
                {
                    this._pnlEmpty.gameObject.SetActive(true);
                    this._pnlOccupied.gameObject.SetActive(false);
                    break;
                }
            case SlotState.OCCUPIED:
                {
                    this._pnlOccupied.gameObject.SetActive(true);
                    this._pnlEmpty.gameObject.SetActive(false);
                    break;
                }
        }
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
}

public enum SlotState
{
    EMPTY,
    OCCUPIED,
}
