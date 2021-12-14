using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_normal_animator : MonoBehaviour
{
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
        if (InputMgr.StartShoot(player_mouse))
            animator.SetBool(AnimStates.Mouse.SURIKEN, true);
        else
        if (InputMgr.ToggleJump(player_mouse))
            animator.SetBool(AnimStates.Mouse.JUMPING, true);
        else
        {
            bool didChangePosition = AnimationUtils.DidChangePosition(player_mouse);
            
            if (didChangePosition) animator.SetBool(AnimStates.Mouse.IS_RUNNING, true);
            else animator.SetBool(AnimStates.Mouse.IS_RUNNING, false);
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
