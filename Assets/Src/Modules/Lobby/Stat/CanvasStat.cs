using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasStat : MonoBehaviour
{
    const string IMG_AVATAR = "ImgAvatar";
    const string LB_USERNAME = "LbUsername";
    const string LB_DESCRIPTION = "LbDescription";
    const string BTN_EDIT = "BtnEdit";
    const string TXT_USERNAME = "TxtUsername";
    const string TXT_DESCRIPTION = "TxtDescription";
    const string BTN_CONFIRM = "BtnConfirm";
    const string IMG_RANK = "ImgRank";
    const string LB_RANK_NAME = "LbRankName";
    const string LB_KDA = "LbKda";
    const string LB_HIGHEST_RANK = "LbHighestRank";
    const string LB_NUM_SKIN = "LbNumSkin";
    const string LB_NUM_FRIEND = "LbNumFriend";

    Image _imgAvatar = null;
    Text _lbUsername = null;
    Text _lbDescription = null;
    Button _btnEdit = null;
    InputField _txtUsername = null;
    InputField _txtDescription = null;
    Button _btnConfirm = null;
    Image _imgRank = null;
    Text _lbRankName = null;
    Text _lbKda = null;
    Text _lbHighestRank = null;
    Text _lbNumSkin = null;
    Text _lbNumFiend = null;

    // Start is called before the first frame update
    void Start()
    {
        UiHelper uiHelper = new UiHelper(this.gameObject);
        this._imgAvatar = uiHelper.ui[IMG_AVATAR].GetComponent<Image>();
        this._lbUsername = uiHelper.ui[LB_USERNAME].GetComponent<Text>();
        this._lbDescription = uiHelper.ui[LB_DESCRIPTION].GetComponent<Text>();
        this._btnEdit = uiHelper.ui[BTN_EDIT].GetComponent<Button>();
        this._txtUsername = uiHelper.ui[TXT_USERNAME].GetComponent<InputField>();
        this._txtDescription = uiHelper.ui[TXT_DESCRIPTION].GetComponent<InputField>();
        this._btnConfirm = uiHelper.ui[BTN_CONFIRM].GetComponent<Button>();
        this._imgRank = uiHelper.ui[IMG_RANK].GetComponent<Image>();
        this._lbRankName = uiHelper.ui[LB_RANK_NAME].GetComponent<Text>();
        this._lbKda = uiHelper.ui[LB_KDA].GetComponent<Text>();
        this._lbHighestRank = uiHelper.ui[LB_HIGHEST_RANK].GetComponent<Text>();
        this._lbNumSkin = uiHelper.ui[LB_NUM_SKIN].GetComponent<Text>();
        this._lbNumFiend = uiHelper.ui[LB_NUM_FRIEND].GetComponent<Text>();

        this._btnEdit.onClick.AddListener(this.SetEditMode);
        this._btnConfirm.onClick.AddListener(this.SetViewMode);

        SetViewMode();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetViewMode()
    {
        this._txtUsername.gameObject.SetActive(false);
        this._txtDescription.gameObject.SetActive(false);
        this._btnConfirm.gameObject.SetActive(false);

        this._lbUsername.gameObject.SetActive(true);
        this._lbDescription.gameObject.SetActive(true);
        this._btnEdit.gameObject.SetActive(true);
    }

    void SetEditMode()
    {
        this._txtUsername.gameObject.SetActive(true);
        this._txtDescription.gameObject.SetActive(true);
        this._btnConfirm.gameObject.SetActive(true);

        this._lbUsername.gameObject.SetActive(false);
        this._lbDescription.gameObject.SetActive(false);
        this._btnEdit.gameObject.SetActive(false);
    }
}
