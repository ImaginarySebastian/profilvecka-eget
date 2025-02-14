using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;

public class MovingPlatforms : MonoBehaviour
{
    public GameObject Player;
    Collider2D col;
    int direction = 0;
    float speed = 0f;
    Rigidbody2D rb;
    Rigidbody2D rbPlayer;
    private bool Onplatform = false;
    public float Fliptime;
    public float Flip = 3f;
    public float moveRange = 5f;
    private Vector2 startpos;

  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        RandomValues();
        rbPlayer = Player.GetComponent<Rigidbody2D>();
    
    }
    void Update()
    {
        MovePlatforms();
    }

  


    private void RandomValues()
    {
        direction = Random.Range(1,3);
        speed = 2f;
        if(direction == 2)
        {
            speed = -speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Onplatform = true;
            AttachPlayerToPlatform();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Onplatform && collision.CompareTag("Player"))
        {
            rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.y);
            rbPlayer.gravityScale = 0;
        }


        if (Onplatform && collision.CompareTag("Player"))
        {
            rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.x);
            rbPlayer.gravityScale = 0;

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Onplatform = false;
            DetachPlayerFromPlatform();

             rbPlayer.gravityScale = 1;
        }
    }

    void MovePlatforms()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void DetachPlayerFromPlatform()
    {
        Player.transform.parent = null;
    }

    void AttachPlayerToPlatform()
    {
        Player.transform.parent = transform;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, 0f);
        float newPosX = Mathf.PingPong(Time.time * speed, moveRange);
        transform.position = new Vector2(startpos.x + newPosX, transform.position.y);

        if (Time.time >= Fliptime)
        {
            Flipp();
            Fliptime = Time.time + Flip;
        }
    }

    void Flipp()
    {
        Vector2 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;

    }
}

