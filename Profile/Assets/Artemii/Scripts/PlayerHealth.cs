using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 100;
    [SerializeField] float invincibilityTime = 0.5f;
    bool invincible = false;

    private void DisableInvinciblity()
    {
        invincible = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (invincible == true)
            {
                return;
            }
            if (playerHealth > 0)
            {
                playerHealth -= 10;
                invincible = true;
                Invoke("DisableInvinciblity", invincibilityTime);
                Debug.Log(playerHealth);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
