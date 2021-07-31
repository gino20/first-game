using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator playerAnimator;
    public float speed;
    public float jumpSpeed;
    public LayerMask ground;
    public Collider2D playerCollider;
    public int totalCherry;
    public int totalGem;
    public Text totalCherryText;
    private bool touchingCollection;
    public AudioSource jumpAudio;
    public AudioSource collectAudio;
    public Collider2D enterObjectCollider;
    public GameObject enterDialog;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerAnimator.GetBool("hurting"))
        {
            Move();
        }
        Transition();
        CollectHandler();
    }

    void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float horizontalDirection = Input.GetAxisRaw("Horizontal");



        // 角色移动
        if (horizontalAxis != 0)
        {
            player.velocity = new Vector2(horizontalAxis * speed, player.velocity.y);
            playerAnimator.SetFloat("running", Mathf.Abs(horizontalDirection));
        }
        if (horizontalDirection != 0)
        {
            player.transform.localScale = new Vector3(horizontalDirection, 1, 1);
        }


    }

    void Transition()
    {
        JumpHandler();
        CrouchHandler();

    }

    void JumpHandler()
    {
        bool pressingJumpButton = Input.GetButtonDown("Jump");
        // 跳起
        if (pressingJumpButton && playerAnimator.GetBool("idling"))
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            playerAnimator.SetBool("jumping", true);
            playerAnimator.SetBool("idling", false);
            jumpAudio.Play();
        }

        // 下落
        if (player.velocity.y < 0)
        {
            playerAnimator.SetBool("jumping", false);
            playerAnimator.SetBool("falling", true);
        }

        // 下落结束
        if (playerAnimator.GetBool("falling") && playerCollider.IsTouchingLayers(ground))
        {
            playerAnimator.SetBool("falling", false);
            playerAnimator.SetBool("idling", true);
        }
    }

    void CrouchHandler()
    {
        if (Input.GetButton("Crouch"))
        {
            playerAnimator.SetBool("crouching", true);
        }
        else
        {
            playerAnimator.SetBool("crouching", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collection")
        {
            touchingCollection = true;
            Destroy(other.gameObject);
        }

        if (other.tag == "Entrance")
        {
            enterDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Entrance")
        {
            enterDialog.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            // 消灭敌人
            if (playerAnimator.GetBool("falling"))
            {
                EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
                enemy.BeElimatedHandler();
                // Destroy(other.gameObject);
                player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                playerAnimator.SetBool("jumping", true);
                playerAnimator.SetBool("idling", false);
            }
            else if (player.transform.position.x < other.gameObject.transform.position.x)
            {
                player.velocity = new Vector2(-10, player.velocity.y);
                playerAnimator.SetBool("hurting", true);
            }
            else if (player.transform.position.x > other.gameObject.transform.position.x)
            {
                player.velocity = new Vector2(10, player.velocity.y);
                playerAnimator.SetBool("hurting", true);
            }
        }
    }

    void CollectHandler()
    {
        if (touchingCollection)
        {
            totalCherry += 1;
            totalCherryText.text = totalCherry.ToString();
            touchingCollection = false;
        }
    }

    void AfterHurt()
    {
        playerAnimator.SetBool("hurting", false);
    }
}
