using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, 0f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipSprite();
    }
    void FlipSprite()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
