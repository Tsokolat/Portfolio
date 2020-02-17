using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;


public class Actions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (GameStatus.status.health <= 0)
        {
            GameStatus.status.health = 100;
        }

        SceneManager.LoadScene("Level1");
    }

    public void SaveGame()
    {
        GameStatus.status.Save();
    }

    public void LoadGame()
    {
        GameStatus.status.Load();
    }

    public void ExitGame()
    {
        GameStatus.status.doExitGame();
    }

    public void Credits()
    {
        GameStatus.status.Credits();
    }
}
