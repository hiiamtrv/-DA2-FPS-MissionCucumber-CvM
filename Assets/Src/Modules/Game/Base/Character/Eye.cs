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
        const int LAYER_DEFAULT = 0;
        const int LAYER_SELF = 6;

        float _camRotation = 0f;
        [SerializeField] GameObject _eyePoint;
        [SerializeField] RenderTexture _blindFold;

        [SerializeField] bool _isAI;
        public bool IsAI => _isAI;

        [SerializeField] bool _enableMouse;
        public bool EnableMouse => _enableMouse;

        [SerializeField] GameObject[] _cameras;
        [SerializeField] GameObject[] _charModel;
        public GameObject[] ArrCharModel => _charModel;
        public GameObject CharModel(int index = 0) => _charModel[index];

        [SerializeField] float _height;
        public float Height => _height;

        RotateModel _model;
        PhotonView _view;

        void Start()
        {
            this._view = this.GetComponent<PhotonView>();
            if (!_view.IsMine || _isAI)
            {
                foreach (var camera in _cameras)
                {
                    camera.GetComponent<Camera>().targetTexture = this._blindFold;
                    camera.GetComponent<AudioListener>().enabled = false;
                }

                if (this._charModel != null)
                {
                    foreach (GameObject child in this.ArrCharModel)
                    {
                        Utils.ChangeLayerRecursively(child, LAYER_DEFAULT);
                    }
                }
            }
            else
            {
                if (this._charModel != null)
                {
                    foreach (GameObject child in this.ArrCharModel)
                    {
                        Utils.ChangeLayerRecursively(child, LAYER_SELF);
                    }
                }
            }

            _model = this.GetComponent<CharacterStats>().RotateModel;
            Cursor.lockState = CursorLockMode.Locked;

            EventCenter.Subcribe(EventId.MATCH_END, (pubData) =>
            {
                this._enableMouse = false;
            });
        }

        void Update()
        {
            if (this._view.IsMine && !this._isAI && this._enableMouse)
            {
                float mouseMoveX = Input.GetAxis("Mouse X");
                this.transform.Rotate(new Vector3(0, mouseMoveX, 0) * this._model.RotateSpeed);

                float mouseMoveY = -Input.GetAxis("Mouse Y");
                this._camRotation += mouseMoveY * this._model.RotateSpeed;
            }

            this.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            this._camRotation = Mathf.Clamp(this._camRotation, MIN_ROT_Y, MAX_ROT_Y);
            this._eyePoint.transform.localRotation = Quaternion.Euler(this._camRotation, 0, 0);

            // if (this._view.IsMine && !this._isAI && !MatchTimer.IsStopped && Input.GetKeyDown(KeyBind.CROUCH))
            // {
            //     if (Cursor.lockState == CursorLockMode.Locked)
            //     {
            //         this._enableMouse = false;
            //         Cursor.lockState = CursorLockMode.None;
            //     }
            //     else
            //     {
            //         this._enableMouse = true;
            //         Cursor.lockState = CursorLockMode.Locked;
            //     }
            // }
        }

        public Camera MainCamera
        {
            get
            {
                foreach (var camGameObject in this._cameras)
                    if (camGameObject.tag == "MainCamera") return camGameObject.GetComponent<Camera>();
                return null;
            }
        }

        public void LookAt(GameObject target)
        {
            // GameObject targetObject = (target.TryGetComponent(typeof(Eye), out Component component)
            //     ? target.GetComponent<Eye>().CharModel()
            //     : target
            // );

            Vector3 targetPos = target.transform.position;
            // if (target.TryGetComponent(typeof(Eye), out Component component))
            // {
            //     targetPos += target.GetComponent<Eye>().Height * Vector3.up;
            //     Debug.Log("Look at", targetPos);
            // }

            this.transform.LookAt(targetPos, Vector3.up);
            this.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            this._eyePoint.transform.LookAt(targetPos, Vector3.up);
            this._camRotation = this._eyePoint.transform.localEulerAngles.x;
        }

        public void ResetRotation()
        {
            this._camRotation = 0;
        }

        void OnDisable()
        {
            foreach (var camera in _cameras)
            {
                camera.gameObject.SetActive(false);
            }
        }

        void OnEnable()
        {
            foreach (var camera in _cameras)
            {
                camera.gameObject.SetActive(true);
            }
        }
    }
}