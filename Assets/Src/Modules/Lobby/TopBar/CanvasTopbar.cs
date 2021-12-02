using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTopbar : BaseGui
{
    const string IMG_AVATAR = "ImgAvatar";
    const string BTN_LOBBY = "BtnLobby";
    const string BTN_STATS = "BtnStats";
    const string BTN_SHOP = "BtnShop";
    const string BTN_FEEDBACK = "BtnFeedback";
    const string LB_USER_ID = "LbUserId";
    const string LB_USER_NAME = "LbUserName";

    Image _imgAvatar = null;
    Button _btnLobby = null;
    Button _btnStats = null;
    Button _btnShop = null;
    Button _btnFeedback = null;
    Text _lbUserId = null;
    Text _lbUserName = null;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        this._imgAvatar = uiHelper.ui[IMG_AVATAR].GetComponent<Image>();
        this._btnLobby = uiHelper.ui[BTN_LOBBY].GetComponent<Button>();
        this._btnStats = uiHelper.ui[BTN_STATS].GetComponent<Button>();
        this._btnShop = uiHelper.ui[BTN_SHOP].GetComponent<Button>();
        this._btnFeedback = uiHelper.ui[BTN_FEEDBACK].GetComponent<Button>();
        this._lbUserId = uiHelper.ui[LB_USER_ID].GetComponent<Text>();
        this._lbUserName = uiHelper.ui[LB_USER_NAME].GetComponent<Text>();

        this._btnLobby.onClick.AddListener(this.OnGoToLobby);
        this._btnStats.onClick.AddListener(this.OnGoToStats);
        this._btnShop.onClick.AddListener(this.OnGoToShop);
        this._btnFeedback.onClick.AddListener(this.OnGotoFeedback);

        EventCenter.Subcribe(EventId.PLAYER_PROFILE_CHANGE, (data) => this.UpdatePlayerProfile());
        EventCenter.Subcribe(EventId.NETWORK_CONNECTED, (data) => this.UpdatePlayerProfile());
    }

    void Start()
    {
        this.OnEnter();
    }

    public override void OnEnter()
    {
        this.UpdatePlayerProfile();
    }

    void UpdatePlayerProfile()
    {
        Debug.Log("Update player profile", NetworkLobby.Ins.UserId, NetworkLobby.Ins.UserName);
        this._lbUserId.text = "UserId:@userId".Replace("@userId", NetworkLobby.Ins.UserId);
        this._lbUserName.text = "UserName:@userName".Replace("@userName", NetworkLobby.Ins.UserName);
    }

    void OnGoToLobby()
    {
        UnityEngine.Debug.Log("[CanvasTopbar] go to Lobby");
        Gm.ChangeGui(Gui.MAIN_MENU);
    }

    void OnGoToStats()
    {
        UnityEngine.Debug.Log("[CanvasTopbar] go to Stats");
        Gm.ChangeGui(Gui.STAT);
    }

    void OnGoToShop()
    {
        UnityEngine.Debug.Log("[CanvasTopbar] go to Shop");
    }

    void OnGotoFeedback()
    {
        UnityEngine.Debug.Log("[CanvasTopbar] go to Feedback");
    }
}