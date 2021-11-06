using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEyeCtrl : MonoBehaviour
{
    const float MIN_ROT_Y = -90f;
    const float MAX_ROT_Y = 90f;
    const float SPEED = 100.0f;

    float _camRotation = 0f;
    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this._camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseMoveX = Input.GetAxis("Mouse X");
        this.transform.Rotate(new Vector3(0, mouseMoveX, 0) * Time.deltaTime * SPEED);

        float mouseMoveY = -Input.GetAxis("Mouse Y");
        this._camRotation += mouseMoveY * Time.deltaTime * SPEED;
        this._camera.transform.Rotate(new Vector3(mouseMoveY, 0, 0) * Time.deltaTime * SPEED);

        this.SnapCameraAngle();
    }

    //snap vertical look at determined sight
    void SnapCameraAngle()
    {
        float fixCamRotation = Mathf.Clamp(this._camRotation, MIN_ROT_Y, MAX_ROT_Y);
        this._camera.transform.Rotate(new Vector3(fixCamRotation - this._camRotation, 0, 0));
        this._camRotation = fixCamRotation;
    }
}
