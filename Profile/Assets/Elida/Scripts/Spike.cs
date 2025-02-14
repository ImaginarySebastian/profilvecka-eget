using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Spike : MonoBehaviour
{
    public int Damage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDeath playerDeath = collision.gameObject.GetComponent<PlayerDeath>();
           
            if (playerDeath != null)
            {
                FindFirstObjectByType<GameSession>().TakeLife();
            }
        }
    }
}
