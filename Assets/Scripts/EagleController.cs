using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : EnemyController
{
    // Start is called before the first frame update
    public GameObject topPoint;
    public GameObject bottomPoint;
    private float topY;
    private float bottomY;
    private Rigidbody2D eagle;
    private Collider2D eagleCollider;
    private float speed = 5;
    protected override void Start()
    {
        base.Start();
        // Get Bounding
        topY = topPoint.transform.position.y;
        bottomY = bottomPoint.transform.position.y;
        Destroy(topPoint.gameObject);
        Destroy(bottomPoint.gameObject);

        eagle = GetComponent<Rigidbody2D>();
        eagleCollider = GetComponent<Collider2D>();
        eagle.velocity = new Vector2(0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveHandler();
    }

    void MoveHandler()
    {
        if (eagle.velocity.y > 0 && eagle.transform.position.y > topY)
        {
            eagle.velocity = new Vector2(0, -speed);
        }
        else if (eagle.velocity.y < 0 && eagle.transform.position.y < bottomY)
        {
            eagle.velocity = new Vector2(0, speed);
        }
    }

    // void DieHandler()
    // {
    //     EnemyController enemy = eagle
    // }
}
