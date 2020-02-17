using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    public GameObject player;
    public GameObject gun;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
            gun.transform.parent = player.transform;
        

    }

  


    //void Start()
    //{
    //    gun.transform.parent = player.transform;
    //}

    // Update is called once per frame
    void Update()
    {
       
    }
}
