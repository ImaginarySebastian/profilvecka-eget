using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperEnemy : MonoBehaviour
{
    [SerializeField] CircleCollider2D explosiveZone;
    [SerializeField] float explosiveTime = 1f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(ExplosiveTimer());
    }
    private IEnumerator ExplosiveTimer()
    {
        if (explosiveZone.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            while (explosiveZone.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                Debug.Log("Staying at zone");
                yield return new WaitForSeconds(explosiveTime);
                FindFirstObjectByType<GameSession>().TakeLife();
            }
            Debug.Log("Boom!");
        }
    }
}
