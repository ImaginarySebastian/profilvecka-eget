using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float enemySpeed = 3f;
    [SerializeField] ContactFilter2D groundFilter;
    [SerializeField] BoxCollider2D edge;
    [SerializeField] public float jumpSpeed = 5f;
    bool isGrounded = true;
    GameObject player;
    Rigidbody2D rb;
    float jumpTime = 2f;
    float timeUntilNextJump = 4f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            Debug.Log("Enemy inte dör");
            if (player != null)
            {
                float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
                rb.velocity = new Vector2(direction*enemySpeed,rb.velocity.y);
            }
        }
    }
    private IEnumerator Jump()
    {
        isGrounded = false;
        float directionOfJump = Mathf.Sign(player.transform.position.x - transform.position.x);

        rb.velocity = new Vector2(directionOfJump * enemySpeed, jumpSpeed);
        yield return new WaitForSeconds(jumpTime);

        yield return new WaitForSeconds(timeUntilNextJump);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (edge.IsTouchingLayers(LayerMask.GetMask("Hinder")))
        {
            Debug.Log("Det träffar");
            if (isGrounded)
            {
                Debug.Log("På marken");

                StartCoroutine(Jump());
            }
        }
    }
    private void FixedUpdate()
    {
        isGrounded = rb.IsTouching(groundFilter);
    }
}