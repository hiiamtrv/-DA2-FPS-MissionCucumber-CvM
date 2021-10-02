using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLogin : MonoBehaviour
{
    const string BTN_LOGIN = "BtnLogin";
    const string BTN_EXIT = "BtnExit";
    const string TXT_USERNAME = "TxtUsername";
    const string TXT_PASSWORD = "TxtPassword";

    Button btnLogin = null;
    Button btnExit = null;
    InputField txtUsername = null;
    InputField txtPassword = null;


    // Start is called before the first frame update
    void Start()
    {
        this.btnLogin = GameObject.Find(BTN_LOGIN).GetComponent<Button>();
        this.btnExit = GameObject.Find(BTN_EXIT).GetComponent<Button>();
        this.txtUsername = GameObject.Find(TXT_USERNAME).GetComponent<InputField>();
        this.txtPassword = GameObject.Find(TXT_PASSWORD).GetComponent<InputField>();

        GuiAnimUtils.MoveY(this.txtUsername.gameObject, 500, 1.5f, 0);
        GuiAnimUtils.MoveY(this.txtPassword.gameObject, 500, 1.5f, 0.5f);
        GuiAnimUtils.MoveY(this.btnLogin.gameObject, 500, 1.5f, 1);

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
