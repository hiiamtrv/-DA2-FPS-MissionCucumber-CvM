using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Eye : MonoBehaviour
    {
        const float MIN_ROT_Y = -90f;
        const float MAX_ROT_Y = 90f;

        float _camRotation = 0f;
        [SerializeField] GameObject _eyePoint;

        RotateModel _model;

        // Start is called before the first frame update
        void Start()
        {
            _model = this.GetComponent<Stats>().RotateModel;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
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