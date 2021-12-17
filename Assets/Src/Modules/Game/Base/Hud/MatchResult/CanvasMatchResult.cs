using System.Collections;
using System.Collections.Generic;
using PubData;
using UnityEngine;
using UnityEngine.UI;

namespace GameHud
{
    public class CanvasMatchResult : MonoBehaviour
    {
        UiHelper uiHelper = null;
        Text _lbMiceWin = null;
        Text _lbCatsWin = null;
        Text _lbDraw = null;
        Text _lbWinReason = null;
        Button _btnStay = null;
        Button _btnLeave = null;

        [SerializeField] List<GameObject> _hideHuds;

        [SerializeField] AudioClip _winSound;
        [SerializeField] AudioClip _loseSound;

        void Awake()
        {
            EventCenter.Subcribe(EventId.MATCH_END, this.DisplayMatchEnd);
        }

        void Start()
        {
            this.uiHelper = new UiHelper(this.gameObject);
            this._lbMiceWin = uiHelper.ui[LB_MICE_WIN].GetComponent<Text>();
            this._lbCatsWin = uiHelper.ui[LB_CATS_WIN].GetComponent<Text>();
            this._lbDraw = uiHelper.ui[LB_DRAW].GetComponent<Text>();
            this._lbWinReason = uiHelper.ui[LB_WIN_REASON].GetComponent<Text>();
            this._btnStay = uiHelper.ui[BTN_STAY].GetComponent<Button>();
            this._btnLeave = uiHelper.ui[BTN_LEAVE].GetComponent<Button>();

            this.gameObject.SetActive(false);

            this._btnStay.onClick.AddListener(this.StayAtRoom);
            this._btnLeave.onClick.AddListener(this.LeaveRoom);
        }

        void DisplayMatchEnd(object pubData)
        {
            Cursor.lockState = CursorLockMode.None;

            this.gameObject.SetActive(true);

            MatchEnd data = pubData as MatchEnd;
            this._lbMiceWin.gameObject.SetActive(data.WinSide == CharacterSide.MICE);
            this._lbCatsWin.gameObject.SetActive(data.WinSide == CharacterSide.CATS);
            this._lbDraw.gameObject.SetActive(data.WinSide == CharacterSide.UNDEFINED);
            this._lbWinReason.text = data.WinReason;

            this._hideHuds.ForEach(hud => hud.gameObject.SetActive(false));

            AudioClip endSound;
            if (data.WinSide == GameVar.StartSide) endSound = _winSound;
            else endSound = _loseSound;
            
            AudioSource audio = this.GetComponent<AudioSource>();
            audio.clip = endSound;
            audio.enabled = true;
        }

        void StayAtRoom()
        {
            // NetworkGame.Ins.EndGame(false);
        }

        void LeaveRoom()
        {
            NetworkGame.Ins.EndGame();
        }

        const string LB_MICE_WIN = "LbMiceWin";
        const string LB_CATS_WIN = "LbCatsWin";
        const string LB_DRAW = "LbDraw";
        const string LB_WIN_REASON = "LbWinReason";
        const string BTN_STAY = "BtnStay";
        const string BTN_LEAVE = "BtnLeave";
    }
}
