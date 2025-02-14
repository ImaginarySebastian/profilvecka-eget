using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShotting : MonoBehaviour
{
    [SerializeField] public float BulletSpeed = 10f;
    [SerializeField] GameObject Bullet;
    public float Direction = 1f;


    void Start() 
    {
        Direction = transform.localScale.x > 0 ? 1f : -1f;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnFire();
        }
        if (transform.localScale.x < 0)
        {
            Direction = -1f;
        }
        else
        {
            Direction = 1f;
        }
        Debug.Log(BulletSpeed);
    }
    void OnFire()
    {
        GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent < Rigidbody2D >();
        

        if(rb != null)
        {
            rb.velocity = new Vector2(Direction * BulletSpeed, 0f);
        }

    }

  
}
