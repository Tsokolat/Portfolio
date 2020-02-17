using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour 
{
    Animator anim;
    public AudioSource explosion;

    



    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        collision.GetComponent<PlayerHealth>().MakeDead();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Explosion");
            explosion.Play();
        }

    }

    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject.transform.root.gameObject);
        }
    }
    */

    public void DestroyTrap()
    {
        Destroy(gameObject);
    }



}
