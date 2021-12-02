using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasLogin : MonoBehaviour
{
    const string IMG_BACKGROUND = "ImgBackground";
    const string PNL_LOGIN = "PnlLogin";
    const string BTN_LOGIN = "BtnLogin";
    const string BTN_EXIT = "BtnExit";
    const string TXT_USERNAME = "TxtUsername";
    const string TXT_PASSWORD = "TxtPassword";
    const string BTN_CLEAR_LOCAL_DATA = "BtnClearLocalData";

    UiHelper uiHelper = null;
    Image _imgBackground = null;
    RectTransform _pnlLogin = null;
    Button _btnLogin = null;
    Button _btnExit = null;
    InputField _txtUsername = null;
    InputField _txtPassword = null;
    Button _btnClearLocalData = null;


    // Start is called before the first frame update
    void Start()
    {
        this.uiHelper = new UiHelper(this.gameObject);
        this._imgBackground = uiHelper.ui[IMG_BACKGROUND].GetComponent<Image>();
        this._pnlLogin = uiHelper.ui[PNL_LOGIN].GetComponent<RectTransform>();
        this._btnLogin = uiHelper.ui[BTN_LOGIN].GetComponent<Button>();
        this._btnExit = uiHelper.ui[BTN_EXIT].GetComponent<Button>();
        this._txtUsername = uiHelper.ui[TXT_USERNAME].GetComponent<InputField>();
        this._txtPassword = uiHelper.ui[TXT_PASSWORD].GetComponent<InputField>();
        this._btnClearLocalData = uiHelper.ui[BTN_CLEAR_LOCAL_DATA].GetComponent<Button>();

        // GuiAnimUtils.FadeIn(this.imgBackground.gameObject);
        GuiAnimUtils.MoveY(this._pnlLogin.gameObject, 500, 1.5f, 0);
        GuiAnimUtils.MoveY(this._txtUsername.gameObject, 500, 1.5f, 0.5f);
        GuiAnimUtils.MoveY(this._txtPassword.gameObject, 500, 1.5f, 1);
        GuiAnimUtils.MoveY(this._btnLogin.gameObject, 500, 1.5f, 1.5f);

        this._btnLogin.onClick.AddListener(this.OnLogin);
        this._btnExit.onClick.AddListener(this.OnExit);
        this._btnClearLocalData.onClick.AddListener(this.OnClearLocalData);

        this.Init();
    }

    void Init()
    {
        this._txtUsername.text = Gm.PlayerProfile.GetUsername();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnLogin()
    {
        if (this._txtUsername.text.Length == 0) return;
        NetworkLogin.Ins.Login(this._txtUsername.text);
    }

    void OnExit()
    {
        Application.Quit();
    }

    void OnClearLocalData()
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.Debug.Log("[CanvasLogin] Local data has been cleared");
    }
}
