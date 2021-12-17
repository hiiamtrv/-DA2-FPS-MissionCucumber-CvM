using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_angry_animator : MonoBehaviour
{
    const float ANIM_DURATION = 0.18f;

    bool _isJumping = false;
    bool _isShooting = false;

    PhotonView view;
    Animator animator;
    [SerializeField] GameObject player_cat;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        view = this.GetComponent<PhotonView>();
        if (!view.IsMine) this.enabled = false;
        AnimatorInit();
        EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, this.doDeath);
    }
    void AnimatorInit()
    {
        animator.SetBool(AnimStates.Cat.IS_IDLING, true);
        animator.SetBool(AnimStates.Cat.IS_ATTACKING, false);
        animator.SetBool(AnimStates.Cat.IS_RUNNING, false);
        animator.SetBool(AnimStates.Cat.IS_DEATH, false);
        animator.SetBool(AnimStates.Cat.IS_JUMPING, false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!this._isJumping && this._isShooting)
        {
            animator.SetBool(AnimStates.Cat.IS_IDLING, true);
            animator.SetBool(AnimStates.Cat.IS_ATTACKING, false);
            animator.SetBool(AnimStates.Cat.IS_DEATH, false);
            animator.SetBool(AnimStates.Cat.IS_JUMPING, false);
            
            if (InputMgr.StartShoot(player_cat))
            {
                animator.SetBool(AnimStates.Cat.IS_ATTACKING, true);
                this._isShooting = true;
                LeanTween.delayedCall(ANIM_DURATION, () => this._isShooting = false);
            }
            else
            if (InputMgr.ToggleJump(player_cat))
            {
                animator.SetBool(AnimStates.Cat.IS_JUMPING, true);
                this._isJumping = true;
                LeanTween.delayedCall(ANIM_DURATION, () => this._isJumping = false);
            }
            else
            {
                bool didPosChange = AnimationUtils.DidChangePosition(player_cat);

                if (didPosChange) animator.SetBool(AnimStates.Cat.IS_RUNNING, true);
                else animator.SetBool(AnimStates.Cat.IS_RUNNING, false);
            }
        }
    }

    void LateUpdate()
    {
        AnimationUtils.RecordPosition(player_cat);
    }

    void doDeath(object pubData)
    {
        GameObject data = (GameObject)pubData;
        if (data == this.player_cat)
        {
            animator.SetBool(AnimStates.Cat.IS_DEATH, true);
            this.enabled = false;
        }
    }
}
