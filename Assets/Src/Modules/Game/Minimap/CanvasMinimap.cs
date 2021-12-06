using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMinimap : MonoBehaviour
{
    [SerializeField] RawImage _bigMap;
    [SerializeField] Image _miniMap;

    // Update is called once per frame
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
            this._miniMap.gameObject.SetActive(true);
        }
    }
}
