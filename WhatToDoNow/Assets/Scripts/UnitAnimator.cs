using UnityEngine;
using System.Collections;

public class UnitAnimator : MonoBehaviour
{
    Animator animator;
    bool isFacingRight = true;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        float dirSpeed = rigidbody.velocity.x;
        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(dirSpeed), Mathf.Abs(rigidbody.velocity.z)));
        if (dirSpeed > 0 && !isFacingRight ||
            dirSpeed < 0 && isFacingRight)
            Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
