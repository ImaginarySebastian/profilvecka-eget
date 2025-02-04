using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 3f;
    [SerializeField] ContactFilter2D groundFilter;
    bool isGrounded = true;
    GameObject player;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded == true)
        {
            if (player != null)
            {
                Vector2 targetPosition = rb.position;
                Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, 0).normalized;
                targetPosition.x += direction.x * enemySpeed * Time.fixedDeltaTime;
                rb.MovePosition(targetPosition);
            }
        }
    }
    private void FixedUpdate()
    {
        isGrounded = rb.IsTouching(groundFilter);
    }
}
