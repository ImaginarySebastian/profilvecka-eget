using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerDeath : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] bool INVULNERABLE;
    private GameObject gameReset;
    private GameReset gameResetScript;

    private void Awake()
    {
        gameReset = GameObject.FindWithTag("GameReset");
        gameResetScript = gameReset.GetComponent<GameReset>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes") && INVULNERABLE == false)
        {
            Debug.Log("hihi");
            gameResetScript.Died();
        }
    }
}
