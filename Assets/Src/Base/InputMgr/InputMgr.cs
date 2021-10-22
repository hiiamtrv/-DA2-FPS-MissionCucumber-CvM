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
    static bool _useSkill1 = false;
    static bool _useSkill2 = false;

    public static float GetZMove() => _zMove;
    public static float GetXMove() => _xMove;
    public static bool DoShoot() => _shoot;
    public static bool DoJump() => _jump;
    public static bool DoCrouch() => _crouch;
    public static bool DoWalk() => _walk;
    public static bool DoInteract() => _interact;
    public static bool DoUseSkill1() => _useSkill1;
    public static bool DoUseSkill2() => _useSkill2;

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
        _jump = !_jump && Input.GetKey(KeyBind.JUMP);
        _crouch = Input.GetKey(KeyBind.CROUCH);
        _walk = Input.GetKey(KeyBind.WALK);
        _interact = Input.GetKey(KeyBind.INTERACT);
        _useSkill1 = Input.GetKey(KeyBind.SKILL_1);
        _useSkill2 = Input.GetKey(KeyBind.SKILL_2);
    }
}
