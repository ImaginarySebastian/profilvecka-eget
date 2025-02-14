using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movingspike : MonoBehaviour
{
    public float Spikemovingspeed = 3f;
    public float Distans = 4f;
    public float Waittime = 3f;
    private Vector2 startPos;
    private bool Movingdown;
    private bool Iswaiting;
    void Start()
    {
        startPos = transform.position;
    }

  
    void Update()
    {
        if (Iswaiting) return;

        if (Movingdown)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos - Vector2.up * Distans, Spikemovingspeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, startPos - Vector2.up * Distans) < 0.1f)
            {
                Iswaiting = true;
                Invoke("SwitchDirections", Waittime);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, Spikemovingspeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, startPos) < 0.1f)
            {
                Iswaiting = true;
                Invoke("SwitchDirections", Waittime);
            }
        }
    }

    void SwitchDirections()
    {
        Movingdown = !Movingdown;
        Iswaiting = false;
    }

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
