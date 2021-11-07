using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : MonoBehaviour
{
    static float _zMove = 0;
    static float _xMove = 0;
    static bool _shoot = false;
    static bool _jump = false;
    static bool _crouch = false;
    static bool _walk = false;
    static bool _interact = false;
    static bool[] _useSkill = { false, false };

    public static float ZMove => _zMove;
    public static float XMove => _xMove;
    public static bool Shoot => _shoot;
    public static bool Jump => _jump;
    public static bool Crouch => _crouch;
    public static bool Walk => _walk;
    public static bool Interact => _interact;
    public static bool UseSkill(int idx) => _useSkill[idx];

    // Update is called once per frame
    void FixedUpdate()
    {
        this.HandleInput();
    }

    void HandleInput()
    {
        _xMove = Input.GetAxis("Horizontal");
        _zMove = Input.GetAxis("Vertical");
        _shoot = Input.GetMouseButton(KeyBind.FIRE);
        _jump = !_jump && Input.GetKey(KeyBind.JUMP) || (Input.GetAxis("Mouse ScrollWheel") > 0f);
        _crouch = Input.GetKey(KeyBind.CROUCH);
        _walk = Input.GetKey(KeyBind.WALK);
        _interact = Input.GetKey(KeyBind.INTERACT);
        _useSkill[0] = Input.GetKey(KeyBind.SKILL_0);
        _useSkill[1] = Input.GetKey(KeyBind.SKILL_1);
    }
}
