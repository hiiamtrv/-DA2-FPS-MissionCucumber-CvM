using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : BaseGui
{
    const string BTN_GENERAL = "BtnGeneral";
    const string BTN_TERM = "BtnTerm";
    const string BTN_CLOSE = "BtnClose";
    const string PNL_GENERAL = "PnlGeneral";
    const string PNL_TERM = "PnlTerm";
    const string SLIDER_MASTER = "SliderMaster";
    const string SLIDER_MUSIC = "SliderMusic";
    const string SLIDER_SFX = "SliderSfx";

    Button _btnGeneral = null;
    Button _btnTerm = null;
    Button _btnClose = null;
    RectTransform _pnlGeneral = null;
    RectTransform _pnlTerm = null;
    Slider _sliderMaster = null;
    Slider _sliderMusic = null;
    Slider _sliderSfx = null;

    protected override void Awake()
    {
        base.Awake();

        this._btnGeneral = uiHelper.ui[BTN_GENERAL].GetComponent<Button>();
        this._btnTerm = uiHelper.ui[BTN_TERM].GetComponent<Button>();
        this._btnClose = uiHelper.ui[BTN_CLOSE].GetComponent<Button>();
        this._pnlGeneral = uiHelper.ui[PNL_GENERAL].GetComponent<RectTransform>();
        this._pnlTerm = uiHelper.ui[PNL_TERM].GetComponent<RectTransform>();
        this._sliderMaster = uiHelper.ui[SLIDER_MASTER].GetComponent<Slider>();
        this._sliderMusic = uiHelper.ui[SLIDER_MUSIC].GetComponent<Slider>();
        this._sliderSfx = uiHelper.ui[SLIDER_SFX].GetComponent<Slider>();

        this._btnGeneral.onClick.AddListener(this.OpenPnlGeneral);
        this._btnTerm.onClick.AddListener(this.OpenPnlTerm);
        this._btnClose.onClick.AddListener(this.OnClose);

        this._sliderMaster.onValueChanged.AddListener(this.OnChangeSoundMaster);
        this._sliderMusic.onValueChanged.AddListener(this.OnChangeSoundMusic);
        this._sliderSfx.onValueChanged.AddListener(this.OnChangeSoundSfx);
    }

    public override void OnEnter()
    {
        this.GetInitData();
        this.OpenPnlGeneral();
    }

    void GetInitData()
    {
        this._sliderMaster.value = Gm.SoundMgr.GetAll();
        this._sliderMusic.value = Gm.SoundMgr.GetBgm();
        this._sliderSfx.value = Gm.SoundMgr.GetSfx();
    }

    void OpenPnlGeneral()
    {
        this._pnlTerm.gameObject.SetActive(false);
        this._pnlGeneral.gameObject.SetActive(true);
    }

    void OpenPnlTerm()
    {
        this._pnlTerm.gameObject.SetActive(true);
        this._pnlGeneral.gameObject.SetActive(false);
    }

    void OnClose()
    {
        // this.gameObject.SetActive(false);
        Gm.ChangeGui(Gui.MAIN_MENU);
    }

    void OnChangeSoundMaster(float value)
    {
        Gm.SoundMgr.SetAll(value);
    }

    void OnChangeSoundMusic(float value)
    {
        Gm.SoundMgr.SetBgm(value);
    }

    void OnChangeSoundSfx(float value)
    {
        Gm.SoundMgr.SetSfx(value);
    }

    void OnDisable()
    {
        Gm.SoundMgr.SavePrefs();
    }
}
