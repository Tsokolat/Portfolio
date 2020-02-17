using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class SaveAct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        GetComponent<GameStatus>().Save();
    }
}
