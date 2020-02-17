using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCleanerController : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().MakeDead();
        }
    }

    public void RestartGame()
    {
        GameStatus.status.health=100;
        //SceneManager.LoadScene("Level1");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void StopGame()
    {
        Application.Quit();
    }

    public void goMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
