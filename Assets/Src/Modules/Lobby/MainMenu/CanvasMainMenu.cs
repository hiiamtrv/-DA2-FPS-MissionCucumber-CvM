using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : BaseGui
{
    const string BTN_SETTING = "BtnSetting";
    const string BTN_PLAY = "BtnPlay";

    Button btnSetting = null;
    Button btnPlay = null;

    protected override void Awake()
    {
        base.Awake();
        this.btnSetting = uiHelper.ui[BTN_SETTING].gameObject.GetComponent<Button>();
        this.btnPlay = uiHelper.ui[BTN_PLAY].gameObject.GetComponent<Button>();

        this.btnSetting.onClick.AddListener(this.OnOpenSetting);
        this.btnPlay.onClick.AddListener(this.OnPlay);
    }

    void OnOpenSetting()
    {
        UnityEngine.Debug.Log("[CanvasMainMenu] open setting");
        Gm.ChangeGui(Gui.SETTING);
    }

    void OnPlay()
    {
        UnityEngine.Debug.Log("[CanvasMainMenu] open make room");
        Gm.ChangeGui(Gui.MAKE_ROOM);
    }
}
