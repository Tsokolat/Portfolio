using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_Clip: MonoBehaviour
{
    public AudioSource Tele;

    // Start is called before the first frame update
    void Start()
    {
        Tele = GetComponent<AudioSource>();    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Tele.Play();
        }
    }



}
