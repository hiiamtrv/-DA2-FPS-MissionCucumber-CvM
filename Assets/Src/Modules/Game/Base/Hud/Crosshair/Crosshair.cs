using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    static Crosshair _ins;
    public static Crosshair Ins => _ins;

    Image _crosshair;

    void Awake()
    {
        _ins = this;
        _crosshair = this.GetComponent<Image>();
    }

    public void Show()
    {
        _crosshair.enabled = true;
    }

    public void Hide()
    {
        _crosshair.enabled = false;
    }

    public void SetVisible(bool visible)
    {
        _crosshair.enabled = visible;
    }
}
