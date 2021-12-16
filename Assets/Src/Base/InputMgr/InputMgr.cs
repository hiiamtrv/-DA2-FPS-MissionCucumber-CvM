using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Photon.Pun;
using UnityEngine;

public class InputMgr : MonoBehaviour
{
    public static float ZMove(GameObject gameObject)
        => IsPlayer(gameObject) ? Input.GetAxis("Vertical") : 0;
    public static float XMove(GameObject gameObject)
        => IsPlayer(gameObject) ? Input.GetAxis("Horizontal") : 0;

    public static bool Crouch(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetKey(KeyBind.CROUCH);
    public static bool Walk(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetKey(KeyBind.WALK);

    public static bool Reload(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetKeyDown(KeyBind.RELOAD);

    public static bool Shoot(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetMouseButton(KeyBind.FIRE);
    public static bool StartShoot(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetMouseButtonDown(KeyBind.FIRE);
    public static bool EndShoot(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetMouseButtonUp(KeyBind.FIRE);

    public static bool Jump(GameObject gameObject)
        => IsPlayer(gameObject) && (Input.GetKey(KeyBind.JUMP) || (Input.GetAxis("Mouse ScrollWheel") > 0f));
    public static bool ToggleJump(GameObject gameObject)
        => IsPlayer(gameObject) && (Input.GetKeyUp(KeyBind.JUMP) || (Input.GetAxis("Mouse ScrollWheel") > 0f));

    public static bool Interact(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetKey(KeyBind.INTERACT);
    public static bool StartInteract(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetKeyDown(KeyBind.INTERACT);
    public static bool EndInteract(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetKeyUp(KeyBind.INTERACT);

    public static bool UseUtil(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetKey(KeyBind.USE_UTIL);
    public static bool StartUseUtil(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetKeyDown(KeyBind.USE_UTIL);
    public static bool EndUseUtil(GameObject gameObject)
        => IsPlayer(gameObject) && Input.GetKeyUp(KeyBind.USE_UTIL);

    public static bool ViewMap => Input.GetKey(KeyBind.MAP);

    public static bool IsPlayer(GameObject gameObject)
    {
        if (gameObject == null) return false;
        else
        {
            PhotonView view = gameObject.GetComponent<PhotonView>();
            Eye eye = gameObject.GetComponent<Eye>();
            return (view != null && view.IsMine && (eye ? !eye.IsAI : true));
        }
    }

    void Awake()
    {
        this.useGUILayout = false;
        EventCenter.Subcribe(EventId.MATCH_END, (e) =>
        {
            Destroy(this);
        });
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
