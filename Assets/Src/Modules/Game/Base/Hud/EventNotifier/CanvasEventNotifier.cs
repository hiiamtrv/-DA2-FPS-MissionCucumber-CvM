using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using Weapons;
using UnityEngine.UI;
using PubData;
using System;

namespace GameHud
{
    public class CanvasEventNotifier : MonoBehaviour
    {
        const string PNL_EVENT = "PnlEvent";
        const string LB_EVENT = "LbEvent";

        UiHelper uiHelper = null;
        RectTransform _pnlEvent = null;
        int _cucumberObtained = 0;

        HashSet<int> _occupiedIndexes = new HashSet<int>();

        const float NOTI_DURATION = 4;
        const float NOTI_HEIGHT = 100;
        const float NOTI_WIDTH = 400;

        void Awake()
        {

        }

        void Start()
        {
            this.uiHelper = new UiHelper(this.gameObject);
            this._pnlEvent = uiHelper.ui[PNL_EVENT].GetComponent<RectTransform>();

            EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, this.OnCharEliminated);
            EventCenter.Subcribe(EventId.CUCUMBER_OBTAINED, this.OnCucumberObtained);
            EventCenter.Subcribe(EventId.CAT_DOWN, this.OnCatDown);
            EventCenter.Subcribe(EventId.CAT_DYING, this.OnCatDying);
            EventCenter.Subcribe(EventId.SHIELD_CENTER_DESTROYED, this.OnShieldCenterDestroyed);
        }

        void OnCharEliminated(object pubData)
        {
            Debug.Log("A char has been eliminated !");
            GameObject dieChar = pubData as GameObject;
            CharacterSide side = dieChar.GetComponent<CharacterStats>().CharacterSide;
            string dieCharName = "";
            switch (side)
            {
                case CharacterSide.CATS:
                    dieCharName = "cat";
                    break;
                case CharacterSide.MICE:
                    dieCharName = "mouse";
                    break;
            }
            string notiContent = "A @dieCharName has been eliminated !".Replace("@dieCharName", dieCharName);
            Debug.Log(notiContent);
            this.DisplayInfomation(notiContent);
        }

        void OnCucumberObtained(object pubData)
        {
            CharacterSide side = GameVar.Ins.Player.GetComponent<CharacterStats>().CharacterSide;
            string replaceString = "";
            switch (side)
            {
                case CharacterSide.CATS:
                    replaceString = "lost";
                    break;
                case CharacterSide.MICE:
                    replaceString = "captured successfully";
                    break;
            }

            this._cucumberObtained++;

            string notiContent = "A cucumber has been @replace ! @numCucumber left !"
                .Replace("@replace", replaceString)
                .Replace("@numCucumber", this._cucumberObtained.ToString());

            this.DisplayInfomation(notiContent);
        }

        void OnCatDown(object pubData)
        {
            string notiContent = "A cat has been down! He would recover soon.";
            this.DisplayInfomation(notiContent);
        }

        void OnCatDying(object pubData)
        {
            CharacterSide side = GameVar.Ins.Player.GetComponent<CharacterStats>().CharacterSide;
            string replaceString = "";
            switch (side)
            {
                case CharacterSide.CATS:
                    replaceString = "Support him!";
                    break;
                case CharacterSide.MICE:
                    replaceString = "Go hide!";
                    break;
            }
            string notiContent = "A cat has sacrified his life for kills. @replace"
                .Replace("@replace", replaceString);
            this.DisplayInfomation(notiContent);
        }

        void OnShieldCenterDestroyed(object pubData)
        {
            string notiContent = "The shield center is destroyed. Cats lose all their shield!";
            this.DisplayInfomation(notiContent);
        }

        void DisplayInfomation(string notiContent)
        {
            int index = 0;
            while (_occupiedIndexes.Contains(index)) index++;
            _occupiedIndexes.Add(index);

            GameObject pnObject = Instantiate(this._pnlEvent.gameObject, this._pnlEvent.transform.position, Quaternion.identity, this._pnlEvent.parent);
            pnObject.transform.localPosition = new Vector2(0, index * -NOTI_HEIGHT);
            pnObject.SetActive(true);

            Text lbEvent = pnObject.transform.Find(LB_EVENT).GetComponent<Text>();
            lbEvent.text = notiContent;

            GuiAnimUtils.MoveX(pnObject, NOTI_WIDTH, 0.5f);
            LeanTween.delayedCall(NOTI_DURATION, () =>
            {
                Destroy(pnObject);
                _occupiedIndexes.Remove(index);
            });
        }
    }
}
