using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : EnemyController
{
    private Rigidbody2D frog;
    private Collider2D frogCollider;
    private Animator frogAnimator;

    public LayerMask ground;
    public GameObject leftPoint;
    public GameObject rightPoint;
    // Start is called before the first frame update
    private float leftX;
    private float rightX;
    private float speed = 5;
    private float jumpForce = 5;
    protected override void Start()
    {
        base.Start();
        frog = GetComponent<Rigidbody2D>();
        frogAnimator = GetComponent<Animator>();
        frogCollider = GetComponent<Collider2D>();

        leftX = leftPoint.transform.position.x;
        rightX = rightPoint.transform.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        MoveHandler();
        JumpHandler();
    }



    void MoveHandler()
    {
        if (frog.transform.localScale.x > 0)
        {
            frog.velocity = new Vector2(-speed, frog.velocity.y);
            if (frog.transform.position.x < leftX)
            {
                frog.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (frog.transform.localScale.x < 0)
        {
            frog.velocity = new Vector2(speed, frog.velocity.y);
            if (frog.transform.position.x > rightX)
            {
                frog.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void JumpHandler()
    {
        if (frogCollider.IsTouchingLayers(ground))
        {
            frogAnimator.SetBool("jumpingUp", true);
            frog.velocity = new Vector2(frog.velocity.x, jumpForce);
        }

        if (frogAnimator.GetBool("jumpingUp") && frog.velocity.y < 0)
        {
            frogAnimator.SetBool("jumpingUp", false);
            frogAnimator.SetBool("fallingDown", true);
        }

        if (frogAnimator.GetBool("fallingDown") && frogCollider.IsTouchingLayers(ground))
        {
            frogAnimator.SetBool("fallingDown", false);
        }
    }
}
