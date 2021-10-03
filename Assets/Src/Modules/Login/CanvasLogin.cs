using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLogin : MonoBehaviour
{
    const string IMG_BACKGROUND = "ImgBackground";
    const string PNL_LOGIN = "PnlLogin";
    const string BTN_LOGIN = "BtnLogin";
    const string BTN_EXIT = "BtnExit";
    const string TXT_USERNAME = "TxtUsername";
    const string TXT_PASSWORD = "TxtPassword";

    Image _imgBackground = null;
    RectTransform _pnlLogin = null;
    Button _btnLogin = null;
    Button _btnExit = null;
    InputField _txtUsername = null;
    InputField _txtPassword = null;


    // Start is called before the first frame update
    void Start()
    {
        this._imgBackground = GameObject.Find(IMG_BACKGROUND).GetComponent<Image>();
        this._pnlLogin = GameObject.Find(PNL_LOGIN).GetComponent<RectTransform>();
        this._btnLogin = GameObject.Find(BTN_LOGIN).GetComponent<Button>();
        this._btnExit = GameObject.Find(BTN_EXIT).GetComponent<Button>();
        this._txtUsername = GameObject.Find(TXT_USERNAME).GetComponent<InputField>();
        this._txtPassword = GameObject.Find(TXT_PASSWORD).GetComponent<InputField>();

        // GuiAnimUtils.FadeIn(this.imgBackground.gameObject);
        GuiAnimUtils.MoveY(this._pnlLogin.gameObject, 500, 1.5f, 0);
        GuiAnimUtils.MoveY(this._txtUsername.gameObject, 500, 1.5f, 0.5f);
        GuiAnimUtils.MoveY(this._txtPassword.gameObject, 500, 1.5f, 1);
        GuiAnimUtils.MoveY(this._btnLogin.gameObject, 500, 1.5f, 1.5f);

        this._btnLogin.onClick.AddListener(this.OnLogin);
        this._btnExit.onClick.AddListener(this.OnExit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnLogin()
    {
        Debug.Log("ALO LOGIN !!!");
    }

    void OnExit()
    {
        Application.Quit();
    }
}
