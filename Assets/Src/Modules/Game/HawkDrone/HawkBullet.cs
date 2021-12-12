using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class HawkBullet : MonoBehaviour
{
    [SerializeField] float _effectTime;
    [SerializeField] Image _imageIndicator;
    GameObject _target;

    void Start()
    {
        float delay = Mathf.Max(Time.deltaTime, _effectTime);
        LeanTween.delayedCall(delay, () =>
        {
            Utils.DestroyGO(this.gameObject);
        });
    }

    void Update()
    {
        if (_target != null)
        {
            this.transform.position = _target.transform.position;
            
            _imageIndicator.gameObject.SetActive(true);
            Vector3 posScreen = Camera.main.WorldToScreenPoint(this.transform.position);
            this._imageIndicator.transform.position = posScreen;
        }
        else
        {
            _imageIndicator.gameObject.SetActive(false);
        }
    }

    public void SetTarget(GameObject target)
    {
        this._target = target;
    }
}
