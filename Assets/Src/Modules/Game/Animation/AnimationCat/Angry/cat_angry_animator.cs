using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_angry_animator : MonoBehaviour
{
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
        animator.SetBool("isIdling", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isDeath", false);
        animator.SetBool("isJumping", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (InputMgr.StartShoot(player_cat))
            animator.SetBool("isAttacking", true);
        else
        if (InputMgr.Jump(player_cat))
            animator.SetBool("isJumping", true);
        else
        {
            bool didPosChange = AnimationUtils.DidChangePosition(player_cat);

            if (didPosChange) animator.SetBool("isRunning", true);
            else animator.SetBool("isRunning", false);
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
            animator.SetBool("isDeath", true);
        }
    }
}
