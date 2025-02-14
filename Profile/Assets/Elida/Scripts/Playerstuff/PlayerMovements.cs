using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpSpeed = 3;
    [SerializeField] ContactFilter2D groundFilter;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    bool canDash = true;
    Vector2 moveInput;
    Rigidbody2D rb;
    bool isGrounded = true;
    float originalGravity;
    float lastDirection = 1;
    Animator ani;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        ani = GetComponent<Animator>();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnJump()
    {
        if (isGrounded == true)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        if (moveInput.x != 0)
        {
            FlipPlayer(moveInput.x);
        }
    }
    void FlipPlayer(float moveDirection)
    {
        if (moveDirection != 0 && Mathf.Sign(moveDirection) != Mathf.Sign(transform.localScale.x))
        {
            transform.localScale = new Vector2(Mathf.Sign(moveDirection), transform.localScale.y);
        }
        

    }
    private IEnumerator Dash()
    {
        if (!canDash) yield break;
        canDash = false;

        float dashDirection = moveInput.x != 0 ? Mathf.Sign(moveInput.x) : lastDirection;
        lastDirection = dashDirection;

        float startTime = Time.time;
        rb.gravityScale = originalGravity * 0.9f; 

        while (Time.time < startTime + dashDuration)
        {
            rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y); 
            yield return null; 
        }

        rb.gravityScale = originalGravity; 
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private void OnDash()
    {   
        StartCoroutine(Dash());
    }
    void Update()
    {
        if (moveInput.x != 0)
        {
            ani.SetBool("isMoving", true);
            lastDirection = moveInput.x;
        }
        else
        {
            ani.SetBool("isMoving", false);
        }
        Debug.Log(lastDirection);
        Run();
    }
    private void OnPause()
    {
        FindObjectOfType<HUDen>().PauseMode();
    }
    private void FixedUpdate()
    {
        isGrounded = rb.IsTouching(groundFilter);
    }
}
