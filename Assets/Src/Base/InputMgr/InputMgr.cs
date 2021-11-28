using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : MonoBehaviour
{
    public static float ZMove => Input.GetAxis("Vertical");
    public static float XMove => Input.GetAxis("Horizontal");

    public static bool Crouch => Input.GetKey(KeyBind.CROUCH);
    public static bool Walk => Input.GetKey(KeyBind.WALK);

    public static bool Reload => Input.GetKeyDown(KeyBind.RELOAD);

    public static bool Shoot => Input.GetMouseButton(KeyBind.FIRE);
    public static bool StartShoot => Input.GetMouseButtonDown(KeyBind.FIRE);
    public static bool EndShoot => Input.GetMouseButtonUp(KeyBind.FIRE);

    public static bool Jump => Input.GetKey(KeyBind.JUMP) || (Input.GetAxis("Mouse ScrollWheel") > 0f);
    public static bool ToggleJump => Input.GetKeyDown(KeyBind.JUMP) || (Input.GetAxis("Mouse ScrollWheel") > 0f);

    public static bool Interact => Input.GetKey(KeyBind.INTERACT);
    public static bool StartInteract => Input.GetKeyDown(KeyBind.INTERACT);
    public static bool EndInteract => Input.GetKeyUp(KeyBind.INTERACT);

    public static bool UseUtil => Input.GetKey(KeyBind.USE_UTIL);
    public static bool StartUseUtil => Input.GetKeyDown(KeyBind.USE_UTIL);
    public static bool EndUseUtil => Input.GetKeyUp(KeyBind.USE_UTIL);

    void Awake()
    {
        this.useGUILayout = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.HandleInput();
    }

    void HandleInput()
    {
        
    }
}
