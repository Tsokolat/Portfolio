using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectActions : MonoBehaviour
{
    public string Level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel ()
    {
        SceneManager.LoadScene(Level);
    }

    
}
