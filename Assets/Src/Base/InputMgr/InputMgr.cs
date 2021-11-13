using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : MonoBehaviour
{
    const float ACCEPTABLE_GAP = 0.2f;
    static float _curUnix;

    static float _zMove = 0;
    public static float ZMove => _zMove;

    static float _xMove = 0;
    public static float XMove => _xMove;

    static bool _shoot = false;
    public static bool Shoot => _shoot;

    static float _unixToggleShoot = -1;
    public static bool ToggleShoot => (_curUnix - _unixToggleShoot <= ACCEPTABLE_GAP);

    static bool _jump = false;
    public static bool Jump => _jump;

    static float _unixToggleJump = -1;
    public static bool ToggleJump => (_curUnix - _unixToggleJump <= ACCEPTABLE_GAP);

    static bool _crouch = false;
    public static bool Crouch => _crouch;

    static bool _walk = false;
    public static bool Walk => _walk;

    static bool _interact = false;
    public static bool Interact => _interact;

    static float _unixToogleInteract = -1;
    public static bool ToggleInteract => (_curUnix - _unixToogleInteract <= ACCEPTABLE_GAP);

    static bool _useUtil = false;
    public static bool UseUtil => _useUtil;

    static float _unixToggleUseUtil = -1;
    public static bool ToggleUseUtil => (_curUnix - _unixToggleUseUtil <= ACCEPTABLE_GAP);

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
        float curUnix = Time.time;
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        bool shoot = Input.GetMouseButton(KeyBind.FIRE);
        bool jump = Input.GetKey(KeyBind.JUMP) || (Input.GetAxis("Mouse ScrollWheel") > 0f);
        bool crouch = Input.GetKey(KeyBind.CROUCH);
        bool walk = Input.GetKey(KeyBind.WALK);
        bool interact = Input.GetKey(KeyBind.INTERACT);
        bool useUtil = Input.GetKey(KeyBind.USE_UTIL);

        _curUnix = curUnix;
        _unixToggleJump = !_jump && jump ? curUnix : -1;
        _unixToggleShoot = !_shoot && shoot ? curUnix : -1;
        _unixToggleUseUtil = !_useUtil && useUtil ? curUnix : -1;

        _xMove = xMove;
        _zMove = zMove;
        _shoot = shoot;
        _jump = jump;
        _crouch = crouch;
        _walk = walk;
        _interact = interact;
        _useUtil = useUtil;
    }
}
