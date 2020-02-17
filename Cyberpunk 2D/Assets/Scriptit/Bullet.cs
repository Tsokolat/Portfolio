using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 1;
  
    //Osumisanimaatiota varten!
    // public GameObject impactEffect;
   
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    //private void OnTriggerEnter2D(Collider2D hitInfo)
    //{
    //    Enemy enemy = hitInfo.GetComponent<Enemy>();
    //    if (enemy != null)
    //    {
    //        enemy.TakeDamage(damage);
    //    }
    //    //Alla oleva koodi osumisanimaatiota varten!
    //    // Instantiate(impactEffect, transform.position, transform.rotation);
    //    Destroy(gameObject); 
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealth>().AddDamage(damage);
            Destroy(gameObject);
        }
    }

}
