using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;




public class GameStatus : MonoBehaviour
{
    Vector3 originalCameraPosition;

    float shakeAmt = 0;

    public Camera mainCamera;

    public static GameStatus status;
    public static string currentLevel;



    public float fullHealth; //maxHealth
    public float currentHealth;    //previoushealth
    public float health;


    // Näiden muuttujien nimet pitää olla samat kuin tasojen nimet.
    public bool Level1;
    public bool Level2;
    public bool Level3;

    void Awake()
    {
        //Tämä varmistaa sen että on olemassa vain yksi gamestatus olio
        //Jos peli yirttää tehdä toisen se tuhotaan välittömästi
        if (status == null)
        {
            DontDestroyOnLoad(gameObject);
            status = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }


    void Update()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }

        //Debug.Log("Current level is:" + currentLevel);
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.health = health;
        data.currentHealth = currentHealth;
        data.fullHealth = fullHealth;
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;


        bf.Serialize(file, data);
        file.Close();//sulkeekansion ja suojaa hakkereilta

    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            health = data.health;
            currentHealth = data.currentHealth;
            fullHealth = data.fullHealth;


            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;
        }

    }


    public void doExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }





    [System.Serializable]
    class PlayerData
    {
        public float health;
        public float currentHealth;
        public float fullHealth;
        public bool Level1;
        public bool Level2;
        public bool Level3;
    }


    public void RunShake(float shke)
    {
        shakeAmt = shke;
        InvokeRepeating("CameraShake", 0, 0.01f);
        Invoke("StopShaking", 0.2f);
    }

    public void CameraShake()
    {
        if (shakeAmt > 0)
        {
            
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;//can also add to x and /or z!
            pp.x += quakeAmt;
            //pp.z += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }


    void StopShaking()
    {
        CancelInvoke("CameraShake");
        //mainCamera.transform.position = originalCameraPosition;
    }

}
