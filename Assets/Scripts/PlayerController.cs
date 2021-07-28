using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator animator;
    public float speed;
    public float jumpSpeed;
    public LayerMask ground;
    public Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Transition();
    }

    void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float horizontalDirection = Input.GetAxisRaw("Horizontal");
        bool isJumpButtonDown = Input.GetButtonDown("Jump");


        // 角色移动
        if (horizontalAxis != 0)
        {
            player.velocity = new Vector2(horizontalAxis * speed, player.velocity.y);
            animator.SetFloat("running", Mathf.Abs(horizontalDirection));
        }
        if (horizontalDirection != 0)
        {
            player.transform.localScale = new Vector3(horizontalDirection, 1, 1);
        }

        // 角色跳跃
        if (isJumpButtonDown)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            animator.SetBool("jumping", true);
        }
    }

    void Transition()
    {
        // animator.SetBool("idling", false);
        toJump();
        toIdle();
    }

    void toJump()
    {
        if (animator.GetBool("jumping") && player.velocity.y < 0)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
        }
    }

    void toIdle()
    {
        if (animator.GetBool("falling") && playerCollider.IsTouchingLayers(ground))
        {
            animator.SetBool("falling", false);
            animator.SetBool("idling", true);
        }
    }
}
