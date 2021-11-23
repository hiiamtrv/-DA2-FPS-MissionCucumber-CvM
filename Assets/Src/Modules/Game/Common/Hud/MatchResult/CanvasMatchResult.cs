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

        [SerializeField] List<GameObject> _hideHuds;

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

            this.gameObject.SetActive(false);
        }

        void DisplayMatchEnd(object pubData)
        {
            this.gameObject.SetActive(true);

            MatchEnd data = pubData as MatchEnd;
            this._lbMiceWin.gameObject.SetActive(data.WinSide == PlayerSide.MICE);
            this._lbCatsWin.gameObject.SetActive(data.WinSide == PlayerSide.CATS);
            this._lbDraw.gameObject.SetActive(data.WinSide == PlayerSide.UNDEFINED);
            this._lbWinReason.text = data.WinReason;

            this._hideHuds.ForEach(hud => hud.gameObject.SetActive(false));
        }

        const string LB_MICE_WIN = "LbMiceWin";
        const string LB_CATS_WIN = "LbCatsWin";
        const string LB_DRAW = "LbDraw";
        const string LB_WIN_REASON = "LbWinReason";
    }
}
