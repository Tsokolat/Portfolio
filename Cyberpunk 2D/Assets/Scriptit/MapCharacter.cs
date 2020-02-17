using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour
{
    public float speed;
   
    void Start()
    {
        //Tarkistetaan onko juuri päästy tasoläpi kun tullaan karttaan.
       if(GameStatus.currentLevel != null)
        {
            GameObject.Find(GameStatus.currentLevel).GetComponent<LoadLevel>().Cleared(true);

            transform.position = GameObject.Find(GameStatus.currentLevel).transform.GetChild(0).transform.position;
        }

       

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
    }

    //Jos pelaaja osuu kartassa olevaan triggeriin käynnistetään taso;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelTrigger") && collision.GetComponent<LoadLevel>().cleared == false)
        {

            // Tallenetaan GameStatukselle kappaleen nimi että teiedetään missä levelissä ollaan
            GameStatus.currentLevel = collision.gameObject.name;
            SceneManager.LoadScene(collision.GetComponent<LoadLevel>().LevelToLoad);
        }
    }
}
