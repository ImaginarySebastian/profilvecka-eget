using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BossTrigger : MonoBehaviour
{
    public VideoPlayer cutscenePlayer; // Dra in VideoPlayer från Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kollar om det är spelaren
        {
            Debug.Log("asfasfasfaf");
            cutscenePlayer.gameObject.SetActive(true); // Visa VideoPlayer
            Time.timeScale = 0;
            cutscenePlayer.Play(); 
            cutscenePlayer.loopPointReached += LoadMainMenu; 
        }
    }
    void LoadMainMenu(VideoPlayer vp)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); 
    }
}
