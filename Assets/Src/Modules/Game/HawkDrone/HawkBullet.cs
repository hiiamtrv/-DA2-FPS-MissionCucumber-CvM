using System.Collections;
using System.Collections.Generic;
using Character;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class HawkBullet : MonoBehaviour
{
    [SerializeField] float _effectTime;
    [SerializeField] Image _imageIndicator;

    GameObject _target;
    bool _isTargetSet = false;
    PhotonView view;

    void Start()
    {
        view = this.GetComponent<PhotonView>();

        float delay = Mathf.Max(Time.deltaTime, _effectTime);
        LeanTween.delayedCall(delay, () =>
        {
            Utils.DestroyGO(this.gameObject);
        });

        this.RunAnimBlink();
    }

    void Update()
    {
        GameObject player = GameVar.Ins.Player;
        Camera pEye = player.GetComponent<Eye>().MainCamera;
        if (!view.IsMine || (_target != null && _target.activeInHierarchy))
        {
            this._imageIndicator.gameObject.SetActive(true);

            if (view.IsMine)
            {
                this.transform.position = _target.transform.position;
                this.transform.Translate(Vector3.up, Space.Self);
            }

            Vector3 posScreen = pEye.WorldToScreenPoint(this.transform.position);
            this._imageIndicator.transform.position = posScreen;
            this._imageIndicator.gameObject.SetActive(this._imageIndicator.transform.position.z > 0);
        }
        else
        {
            this._imageIndicator.gameObject.SetActive(false);
            if (this._isTargetSet)
            {
                Utils.DestroyGO(this.gameObject);
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        this._isTargetSet = true;
        this._target = target;
    }

    void RunAnimBlink()
    {
        if (this == null || this.gameObject == null || this._imageIndicator.gameObject == null) return;

        this._imageIndicator.gameObject.SetActive(false);
        LeanTween.delayedCall(1, () => this._imageIndicator.gameObject.SetActive(true));
        LeanTween.delayedCall(2, this.RunAnimBlink);
    }
}
