using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamCtrl : MonoBehaviour
{
    [SerializeField] GameObject[] camPoint;

    int _numCam;
    int _curCam;
    bool _isMoving = false;

    Camera _camera;
    AudioListener _audioListener;

    bool GoLeft => Input.GetKey(KeyCode.A) || Input.GetMouseButton(0);
    bool GoRight => Input.GetKey(KeyCode.D) || Input.GetMouseButton(1);
    bool OneDirection => (GoLeft ^ GoRight);

    // Start is called before the first frame update
    void Start()
    {
        this._numCam = this.camPoint.Length;
        this._camera = this.GetComponent<Camera>();
        this._camera.enabled = false;

        this._audioListener = this.GetComponent<AudioListener>();
        this._audioListener.enabled = false;

        EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, (e) =>
        {
            GameObject deadChar = e as GameObject;
            if (deadChar == GameVar.Ins.Player)
            {
                this._camera.enabled = true;
                this._audioListener.enabled = true;
                this._curCam = 0;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!this._camera.enabled || this._isMoving) return;

        int nextCam = this._curCam;
        if (this.OneDirection)
        {
            if (this.GoLeft) nextCam--;
            else if (this.GoRight) nextCam++;
        }

        nextCam = (nextCam + _numCam) % _numCam;

        this._curCam = nextCam;
        GameObject camAnchor = camPoint[this._curCam];
        Vector3 camPos = camAnchor.transform.position;
        Quaternion camRot = camAnchor.transform.rotation;

        this._isMoving = true;
        LeanTween.delayedCall(0.5f, () =>
        {
            this._isMoving = false;
        });
        LeanTween.move(this.gameObject, camPos, 0.5f).setEaseInBack();
        LeanTween.rotate(this.gameObject, camRot.eulerAngles, 0.5f);
    }
}
