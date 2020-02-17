using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{


    private void OnGUI()
    {
        if(GUI.Button( new Rect(900, 170, 225, 30),"Play Game"))
        {
            SceneManager.LoadScene("Level1");
        }
        if (GUI.Button(new Rect(900, 220, 225, 30), "Save"))
        {
            GameStatus.status.Save();
        }
        if (GUI.Button(new Rect(900, 270, 225, 30), "Load"))
        {
            GameStatus.status.Load();
        }
        if (GUI.Button(new Rect(900, 320, 225, 30), "Exit")) 
        {
            GameStatus.status.doExitGame();
        }
    }
    
}

