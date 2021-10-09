using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlPlayerSlot : MonoBehaviour
{
    const string LB_PLAYER_NAME = "LbPlayerName";
    const string DEFAULT_NAME = "#DefaultName";

    RectTransform _pnlPlayerSlot = null;
    Text _lbPlayerName = null;
    SlotState _curSlotState;

    // Start is called before the first frame update
    void Start()
    {
        this._pnlPlayerSlot = this.GetComponent<RectTransform>();
        this._lbPlayerName = this.transform.Find(LB_PLAYER_NAME).GetComponent<Text>();

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
        switch (newState)
        {
            case SlotState.EMPTY:
                {
                    this._pnlPlayerSlot.gameObject.SetActive(false);
                    break;
                }
            case SlotState.OCCUPIED:
                {
                    this._pnlPlayerSlot.gameObject.SetActive(true);
                    break;
                }
        }
    }
}

public enum SlotState
{
    EMPTY,
    OCCUPIED,
}
