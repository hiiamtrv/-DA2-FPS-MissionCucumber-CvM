using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using Weapons;
using UnityEngine.UI;
using PubData;

namespace GameHud
{
    public class CanvasPlayerHud : MonoBehaviour
    {
        GameObject _player;

        UiHelper uiHelper = null;
        RectTransform _pnlHealth = null;
        RectTransform _pnlWeapon = null;
        Text _lbHealth = null;
        Text _lbShield = null;
        Text _lbTotalAmmo = null;
        Text _lbRemainAmmo = null;

        void Start()
        {
            this._player = GameVar.Ins.Player;

            this.uiHelper = new UiHelper(this.gameObject);
            this._pnlHealth = uiHelper.ui[PNL_HEALTH].GetComponent<RectTransform>();
            this._pnlWeapon = uiHelper.ui[PNL_WEAPON].GetComponent<RectTransform>();
            this._lbHealth = uiHelper.ui[LB_HEALTH].GetComponent<Text>();
            this._lbShield = uiHelper.ui[LB_SHIELD].GetComponent<Text>();
            this._lbTotalAmmo = uiHelper.ui[LB_TOTAL_AMMO].GetComponent<Text>();
            this._lbRemainAmmo = uiHelper.ui[LB_REMAIN_AMMO].GetComponent<Text>();

            this.GetPlayerInfo();

            EventCenter.Subcribe(EventId.WEAPON_AMMO_EQUIP, this.LoadWeaponInfo);
            EventCenter.Subcribe(EventId.WEAPON_AMMO_CHANGE, this.UpdateRemainAmmo);
            EventCenter.Subcribe(EventId.WEAPON_UNEQUIP, this.HideWeaponPanel);
            EventCenter.Subcribe(EventId.HEALTH_CHANGE, this.UpdatePlayerHealth);
            EventCenter.Subcribe(EventId.SHILED_CHANGE, this.UpdatePlayerShield);
        }

        void GetPlayerInfo()
        {
            CharacterStats stats = this._player.GetComponent<CharacterStats>();
            if (stats != null)
            {
                int health = Mathf.RoundToInt(stats.HealthModel.Health);
                int shield = Mathf.RoundToInt(stats.HealthModel.Shield);
                this._lbHealth.text = health.ToString();
                this._lbShield.text = shield.ToString();
            }
            else
            {
                Debug.LogErrorFormat("[{0}] cannot get stats", this);
                Destroy(this);
            }
        }

        void LoadWeaponInfo(object pubData)
        {
            PubData.WeaponAmmoEquip data = pubData as PubData.WeaponAmmoEquip;
            if (data.Dispatcher == this._player)
            {
                this._pnlWeapon.gameObject.SetActive(true);
                this._lbRemainAmmo.text = data.WeaponModel.RemainAmmo.ToString();
                this._lbTotalAmmo.text = "/" + data.WeaponModel.TotalAmmo.ToString();
            }
        }

        void UpdateRemainAmmo(object pubData)
        {
            WeaponAmmoChange data = pubData as WeaponAmmoChange;
            if (data.Dispatcher == this._player)
            {
                this._lbRemainAmmo.text = data.RemainAmmo.ToString();
                this._lbTotalAmmo.text = "/" + data.TotalAmmo.ToString();
            }
        }

        void HideWeaponPanel(object pubData)
        {
            WeaponUnequip data = pubData as WeaponUnequip;
            if (data.Dispatcher == this._player)
            {
                this._pnlWeapon.gameObject.SetActive(false);
            }
        }

        void UpdatePlayerHealth(object pubData)
        {
            HealthChange data = pubData as HealthChange;
            if (data.Dispatcher == this._player)
            {
                int displayAmount = Mathf.RoundToInt(data.Amount);
                this._lbHealth.text = displayAmount.ToString();
            }
        }

        void UpdatePlayerShield(object pubData)
        {
            ShieldChange data = pubData as ShieldChange;
            if (data.Dispatcher == this._player)
            {
                int displayAmount = Mathf.RoundToInt(data.Amount);
                this._lbShield.text = displayAmount.ToString();
            }
        }

        const string PNL_HEALTH = "PnlHealth";
        const string PNL_WEAPON = "PnlWeapon";
        const string LB_HEALTH = "LbHealth";
        const string LB_SHIELD = "LbShield";
        const string LB_TOTAL_AMMO = "LbTotalAmmo";
        const string LB_REMAIN_AMMO = "LbRemainAmmo";
    }
}
