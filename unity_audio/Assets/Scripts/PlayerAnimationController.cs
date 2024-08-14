using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator anim;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        yield return new WaitForEndOfFrame(); // Ensure the trigger is set before setting the boolean
        anim.SetBool("isGettingUp", true);

        // Wait for the length of the FallingFlatImpact animation
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        anim.SetBool("isGettingUp", false);
    }

    private void OnAnimatorMove()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("FallingDown") || anim.GetCurrentAnimatorStateInfo(0).IsName("GettingUp"))
        {
            rb.MovePosition(anim.rootPosition);
        }
    }
}
