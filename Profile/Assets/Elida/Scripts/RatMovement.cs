using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    public float Ratspeed = 5f;
    public float Flip = 3f;
    public float Fliptime;
    public float moveRange = 5f;
    private Vector2 startpos;
    Rigidbody2D rb;
    GameObject Rat;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startpos = transform.position;
        Fliptime = Time.time + Flip;

    }
    void Update()
    {
        rb.velocity = new Vector2(Ratspeed, 0f);
        float newPosX = Mathf.PingPong(Time.time * Ratspeed, moveRange);
        transform.position = new Vector2(startpos.x + newPosX, transform.position.y);

        if(Time.time >= Fliptime)
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

