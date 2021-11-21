using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using Weapons;
using UnityEngine.UI;

namespace GameHud
{
    public class CanvasPlayerHud : MonoBehaviour
    {
        [SerializeField] GameObject _player;

        UiHelper uiHelper = null;
        RectTransform _pnlHealth = null;
        RectTransform _pnlWeapon = null;
        Text _lbHealth = null;
        Text _lbTotalAmmo = null;
        Text _lbRemainAmmo = null;

        void Start()
        {
            this.uiHelper = new UiHelper(this.gameObject);
            this._pnlHealth = uiHelper.ui[PNL_HEALTH].GetComponent<RectTransform>();
            this._pnlWeapon = uiHelper.ui[PNL_WEAPON].GetComponent<RectTransform>();
            this._lbHealth = uiHelper.ui[LB_HEALTH].GetComponent<Text>();
            this._lbTotalAmmo = uiHelper.ui[LB_TOTAL_AMMO].GetComponent<Text>();
            this._lbRemainAmmo = uiHelper.ui[LB_REMAIN_AMMO].GetComponent<Text>();

            this.SubEvents();
        }

        void SubEvents()
        {
            EventCenter.Subcribe(EventId.WEAPON_EQUIP, this.LoadWeaponInfo);
            EventCenter.Subcribe(EventId.WEAPON_AMMO_CHANGE, this.UpdateRemainAmmo);
            EventCenter.Subcribe(EventId.WEAPON_UNEQUIP, this.HideWeaponPanel);
        }

        void LoadWeaponInfo(object pubData)
        {
            PubData.WeaponEquip data = pubData as PubData.WeaponEquip;
            if (data.Dispatcher == this._player)
            {
                this._pnlWeapon.gameObject.SetActive(true);
                this._lbRemainAmmo.text = data.WeaponModel.RemainAmmo.ToString();
                this._lbTotalAmmo.text = "/" + data.WeaponModel.TotalAmmo.ToString();
            }
        }

        void UpdateRemainAmmo(object pubData)
        {
            PubData.WeaponAmmoChange data = pubData as PubData.WeaponAmmoChange;
            if (data.Dispatcher == this._player)
            {
                this._lbRemainAmmo.text = data.RemainAmmo.ToString();
                this._lbTotalAmmo.text = "/" + data.TotalAmmo.ToString();
            }
        }

        void HideWeaponPanel(object pubData)
        {
            PubData.WeaponUnequip data = pubData as PubData.WeaponUnequip;
            if (data.Dispatcher == this._player)
            {
                this._pnlWeapon.gameObject.SetActive(false);
            }
        }

        const string PNL_HEALTH = "PnlHealth";
        const string PNL_WEAPON = "PnlWeapon";
        const string LB_HEALTH = "LbHealth";
        const string LB_TOTAL_AMMO = "LbTotalAmmo";
        const string LB_REMAIN_AMMO = "LbRemainAmmo";
    }
}
