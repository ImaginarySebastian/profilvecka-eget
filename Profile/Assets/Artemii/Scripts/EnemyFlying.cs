using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    [SerializeField] public float enemySpeed = 3f;
    GameObject player;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y-transform.position.y);
            rb.velocity = new Vector2(direction.x * enemySpeed, direction.y*enemySpeed);
        }
    }
    private void FixedUpdate()
    {
    }
}
