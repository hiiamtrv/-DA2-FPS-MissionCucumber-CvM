using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
    public class CanvasDummyTag : MonoBehaviour
    {
        GameObject _character;
        GameObject _anchor;
        const string TAG_ANCHOR = "TagAnchor";

        UiHelper uiHelper = null;
        RectTransform _pnlStats = null;
        Text _lbHealth = null;
        Text _lbShield = null;

        const string PNL_STATS = "PnlStats";
        const string LB_HEALTH = "LbHealth";
        const string LB_SHIELD = "LbShield";

        void Awake()
        {
            this._character = this.transform.parent.gameObject;
            this._anchor = this._character.transform.Find(TAG_ANCHOR).gameObject;

            EventCenter.Subcribe(EventId.HEALTH_CHANGE, this.ChangeHealth);
            EventCenter.Subcribe(EventId.SHILED_CHANGE, this.ChangeShield);
        }

        void Start()
        {
            this.uiHelper = new UiHelper(this.gameObject);
            this._pnlStats = uiHelper.ui[PNL_STATS].GetComponent<RectTransform>();
            this._lbHealth = uiHelper.ui[LB_HEALTH].GetComponent<Text>();
            this._lbShield = uiHelper.ui[LB_SHIELD].GetComponent<Text>();

            Character.CharacterStats health = this._character.GetComponent<Character.CharacterStats>();
            this._lbHealth.text = "Health: @numHealth".Replace("@numHealth", health.HealthModel.Health.ToString());
            this._lbShield.text = "Shield: @numShield".Replace("@numShield", health.HealthModel.Shield.ToString());
        }

        void Update()
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(this._anchor.transform.position);
            this._pnlStats.transform.position = pos;
        }

        void ChangeHealth(object pubData)
        {
            PubData.HealthChange data = pubData as PubData.HealthChange;
            if (data.Dispatcher == this._character)
                this._lbHealth.text = "Health: @numHealth".Replace("@numHealth", data.Amount.ToString());
        }

        void ChangeShield(object pubData)
        {
            PubData.ShieldChange data = pubData as PubData.ShieldChange;
            if (data.Dispatcher == this._character)
                this._lbShield.text = "Shield: @numShield".Replace("@numShield", data.Amount.ToString());
        }
    }
}
