using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMinimap : MonoBehaviour
{
    [SerializeField] RawImage _bigMap;
    [SerializeField] Image _miniMap;

    bool _viewMiniMap = true;

    void Awake()
    {
        EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, (e) =>
        {
            GameObject dieChar = e as GameObject;
            if (dieChar == GameVar.Ins.Player)
            {
                _viewMiniMap = false;
            }
        });
    }

    void Update()
    {
        if (InputMgr.ViewMap)
        {
            this._bigMap.gameObject.SetActive(true);
            this._miniMap.gameObject.SetActive(false);
        }
        else
        {
            this._bigMap.gameObject.SetActive(false);
            this._miniMap.gameObject.SetActive(_viewMiniMap);
        }
    }
}
