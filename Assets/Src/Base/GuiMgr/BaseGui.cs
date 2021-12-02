using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseGui : MonoBehaviour
{
    protected UiHelper uiHelper = null;

    protected virtual void Awake()
    {
        this.uiHelper = new UiHelper(this.gameObject);
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }
}