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

    Image imgBackground = null;
    RectTransform pnlLogin = null;
    Button btnLogin = null;
    Button btnExit = null;
    InputField txtUsername = null;
    InputField txtPassword = null;


    // Start is called before the first frame update
    void Start()
    {
        this.imgBackground = GameObject.Find(IMG_BACKGROUND).GetComponent<Image>();
        this.pnlLogin = GameObject.Find(PNL_LOGIN).GetComponent<RectTransform>();
        this.btnLogin = GameObject.Find(BTN_LOGIN).GetComponent<Button>();
        this.btnExit = GameObject.Find(BTN_EXIT).GetComponent<Button>();
        this.txtUsername = GameObject.Find(TXT_USERNAME).GetComponent<InputField>();
        this.txtPassword = GameObject.Find(TXT_PASSWORD).GetComponent<InputField>();

        // GuiAnimUtils.FadeIn(this.imgBackground.gameObject);
        GuiAnimUtils.MoveY(this.pnlLogin.gameObject, 500, 1.5f, 0);
        GuiAnimUtils.MoveY(this.txtUsername.gameObject, 500, 1.5f, 0.5f);
        GuiAnimUtils.MoveY(this.txtPassword.gameObject, 500, 1.5f, 1);
        GuiAnimUtils.MoveY(this.btnLogin.gameObject, 500, 1.5f, 1.5f);

        this.btnLogin.onClick.AddListener(this.OnLogin);
        this.btnExit.onClick.AddListener(this.OnExit);
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
