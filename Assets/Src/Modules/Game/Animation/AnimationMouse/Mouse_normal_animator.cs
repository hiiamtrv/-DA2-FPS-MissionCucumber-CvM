using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_normal_animator : MonoBehaviour
{
    const float ANIM_DURATION = 0.75f;

    bool _isJumping = false;
    bool _isShooting = false;

    PhotonView view;
    Animator animator;
    [SerializeField] GameObject player_mouse;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        view = this.GetComponent<PhotonView>();
        if (!view.IsMine) this.enabled = false;
        AnimatorInit();
        EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, this.doDeath);
        EventCenter.Subcribe(EventId.UTILITY_START_COOLDOWN, this.doFireball);
    }
    void AnimatorInit()
    {
        animator.SetBool(AnimStates.Mouse.IS_IDLING, true);
        animator.SetBool(AnimStates.Mouse.SURIKEN, false);
        animator.SetBool(AnimStates.Mouse.IS_RUNNING, false);
        animator.SetBool(AnimStates.Mouse.DIE, false);
        animator.SetBool(AnimStates.Mouse.JUMPING, false);
        animator.SetBool(AnimStates.Mouse.FIREBALL, false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!this._isShooting && !this._isShooting)
        {
            animator.SetBool(AnimStates.Mouse.IS_IDLING, true);
            animator.SetBool(AnimStates.Mouse.SURIKEN, false);
            animator.SetBool(AnimStates.Mouse.IS_RUNNING, false);
            animator.SetBool(AnimStates.Mouse.DIE, false);
            animator.SetBool(AnimStates.Mouse.JUMPING, false);

            if (InputMgr.StartShoot(player_mouse))
            {
                animator.SetBool(AnimStates.Mouse.SURIKEN, true);
                this._isShooting = true;
                LeanTween.delayedCall(ANIM_DURATION, () => this._isShooting = false);
            }
            else if (InputMgr.ToggleJump(player_mouse))
            {
                animator.SetBool(AnimStates.Mouse.JUMPING, true);
                this._isJumping = true;
                LeanTween.delayedCall(ANIM_DURATION, () => this._isJumping = false);
            }
            else
            {
                bool didChangePosition = AnimationUtils.DidChangePosition(player_mouse);

                if (didChangePosition) animator.SetBool(AnimStates.Mouse.IS_RUNNING, true);
                else animator.SetBool(AnimStates.Mouse.IS_RUNNING, false);
            }
        }
    }

    void LateUpdate()
    {
        AnimationUtils.RecordPosition(player_mouse);
    }

    void doDeath(object pubData)
    {
        GameObject data = (GameObject)pubData;
        if (data == this.player_mouse)
        {
            animator.SetBool(AnimStates.Mouse.DIE, true);
            this.enabled = false;
        }
    }
    void doFireball(object pubData)
    {
        PubData.UtilityStartCooldown data = (PubData.UtilityStartCooldown)pubData;
        if (data.Dispatcher == this.player_mouse)
        {
            animator.SetBool(AnimStates.Mouse.FIREBALL, true);
        }
    }

}
