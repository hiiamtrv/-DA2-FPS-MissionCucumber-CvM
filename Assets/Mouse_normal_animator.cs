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
        animator.SetBool("isIdling", true);
        animator.SetBool("suriken", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("die", false);
        animator.SetBool("jumping", false);
        animator.SetBool("fireball", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (InputMgr.StartShoot(player_mouse))
            animator.SetBool("suriken", true);
        else
        if (InputMgr.Jump(player_mouse))
            animator.SetBool("jumping", true);
        else
        {
            float x = InputMgr.XMove(player_mouse);
            float z = InputMgr.ZMove(player_mouse);
            if (z != 0 || x != 0)
                animator.SetBool("isRunning", true);
            else animator.SetBool("isRunning", false);
        }
    }
    void doDeath(object pubData)
    {
        GameObject data = (GameObject)pubData;
        if (data == this.player_mouse)
        {
            animator.SetBool("die", true);
        }
    }
    void doFireball(object pubData)
    {
        PubData.UtilityStartCooldown data = (PubData.UtilityStartCooldown)pubData;
        if (data.Dispatcher == this.player_mouse)
        {
            animator.SetBool("fireball", true);
        }
    }
    
}
