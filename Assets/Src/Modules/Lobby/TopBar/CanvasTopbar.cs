using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTopbar : MonoBehaviour
{
    const string IMG_AVATAR = "ImgAvatar";
    const string BTN_LOBBY = "BtnLobby";
    const string BTN_STATS = "BtnStats";
    const string BTN_SHOP = "BtnShop";
    const string BTN_FEEDBACK = "BtnFeedback";

    Image _imgAvatar = null;
    Button _btnLobby = null;
    Button _btnStats = null;
    Button _btnShop = null;
    Button _btnFeedback = null;

    // Start is called before the first frame update
    void Start()
    {
        UiHelper uiHelper = new UiHelper(this.gameObject);
        this._imgAvatar = uiHelper.ui[IMG_AVATAR].GetComponent<Image>();
        this._btnLobby = uiHelper.ui[BTN_LOBBY].GetComponent<Button>();
        this._btnStats = uiHelper.ui[BTN_STATS].GetComponent<Button>();
        this._btnShop = uiHelper.ui[BTN_SHOP].GetComponent<Button>();
        this._btnFeedback = uiHelper.ui[BTN_FEEDBACK].GetComponent<Button>();

        this._btnLobby.onClick.AddListener(this.OnGoToLobby);
        this._btnStats.onClick.AddListener(this.OnGoToStats);
        this._btnShop.onClick.AddListener(this.OnGoToShop);
        this._btnFeedback.onClick.AddListener(this.OnGotoFeedback);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGoToLobby()
    {
        Debug.Log("[CanvasTopbar] go to Lobby");
    }

    void OnGoToStats()
    {
        Debug.Log("[CanvasTopbar] go to Stats");
    }

    void OnGoToShop()
    {
        Debug.Log("[CanvasTopbar] go to Shop");
    }

    void OnGotoFeedback()
    {
        Debug.Log("[CanvasTopbar] go to Feedback");
    }
}