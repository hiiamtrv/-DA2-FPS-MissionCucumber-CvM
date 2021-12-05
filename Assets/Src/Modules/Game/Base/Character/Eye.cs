using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Character
{
    public class Eye : MonoBehaviour
    {
        const float MIN_ROT_Y = -90f;
        const float MAX_ROT_Y = 90f;

        float _camRotation = 0f;
        [SerializeField] GameObject _eyePoint;
        [SerializeField] RenderTexture _blindFold;

        [SerializeField] GameObject[] _cameras;
        [SerializeField] GameObject _charModel;

        RotateModel _model;
        PhotonView _view;

        void Start()
        {
            this._view = this.GetComponent<PhotonView>();
            Debug.Log("Game object is mine? ", this.gameObject, _view.IsMine);
            if (!_view.IsMine)
            {
                foreach (var camera in _cameras)
                {
                    camera.GetComponent<Camera>().targetTexture = this._blindFold;
                    camera.GetComponent<AudioListener>().enabled = false;
                }
                _charModel.layer = 0;
            }
            else
            {
                _charModel.layer = 6;
            }

            _model = this.GetComponent<CharacterStats>().RotateModel;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (this._view.IsMine)
            {
                float mouseMoveX = Input.GetAxis("Mouse X");
                this.transform.Rotate(new Vector3(0, mouseMoveX, 0) * this._model.RotateSpeed);

                float mouseMoveY = -Input.GetAxis("Mouse Y");
                this._camRotation += mouseMoveY * this._model.RotateSpeed;
                this._camRotation = Mathf.Clamp(this._camRotation, MIN_ROT_Y, MAX_ROT_Y);
                this._eyePoint.transform.localRotation = Quaternion.Euler(this._camRotation, 0, 0);
            }
        }
    }
}