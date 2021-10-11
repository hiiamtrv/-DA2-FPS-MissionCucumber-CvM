using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : MonoBehaviour
{
    const string BTN_SETTING = "BtnSetting";
    const string BTN_PLAY = "BtnPlay";

    Button btnSetting = null;
    Button btnPlay = null;

    // Start is called before the first frame update
    void Start()
    {
        UiHelper uiHelper = new UiHelper(this.gameObject);
        this.btnSetting = uiHelper.ui[BTN_SETTING].gameObject.GetComponent<Button>();
        this.btnPlay = uiHelper.ui[BTN_PLAY].gameObject.GetComponent<Button>();

        this.btnSetting.onClick.AddListener(this.OnOpenSetting);
        this.btnPlay.onClick.AddListener(this.OnPlay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnOpenSetting()
    {
        Debug.Log("[CanvasMainMenu] open setting");
        Gm.ChangeGui(Gui.SETTING);
    }

    void OnPlay()
    {
        Debug.Log("[CanvasMainMenu] open make room");
        Gm.ChangeGui(Gui.MAKE_ROOM);
    }
}
