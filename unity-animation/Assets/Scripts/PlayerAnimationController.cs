using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator anim;

    public void TriggerJump()
    {
        anim.SetTrigger("Jump");
    }

    public void SetJumping(bool isJumping)
    {
        anim.SetBool("isJumping", isJumping);
    }

    public void SetRunning(bool isRunning)
    {
        anim.SetBool("isRunning", isRunning);
    }

    public void SetFalling(bool isFalling)
    {
        anim.SetBool("isFalling", isFalling);
    }

    public void TriggerFallingImpact()
    {
        anim.SetTrigger("FallingImpact");
        StartCoroutine(HandleFallingImpact());
    }

    private IEnumerator HandleFallingImpact()
    {
        // Wait for the FallingImpact animation to complete (duration can be adjusted)
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("isGettingUp", true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("isGettingUp", false);
    }
}
