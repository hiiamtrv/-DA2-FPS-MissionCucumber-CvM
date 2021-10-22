using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEyeCtrl : MonoBehaviour
{
    float _speed = 100.0f;
    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this._camera = this.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotX = Input.GetAxis("Mouse X");
        float rotY = -Input.GetAxis("Mouse Y");
        this.transform.Rotate(new Vector3(0, rotX, 0) * Time.deltaTime * this._speed);
        this._camera.transform.Rotate(new Vector3(rotY, 0, 0) * Time.deltaTime * this._speed);
    }
}
