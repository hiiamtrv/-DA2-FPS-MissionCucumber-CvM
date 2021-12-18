using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseBehaviour : MonoBehaviour
{
    [SerializeField] CharacterSide _side;
    Animator _animator;

    void Start()
    {
        _animator = this.GetComponent<Animator>();

        switch (_side)
        {
            case CharacterSide.CATS:
                _animator.SetBool(AnimStates.Cat.IS_DEATH, true);
                break;
            case CharacterSide.MICE:
                _animator.SetBool(AnimStates.Mouse.DIE, true);
                break;
        }
    }

    public void StopAnimator()
    {
        _animator.enabled = false;
    }
}
