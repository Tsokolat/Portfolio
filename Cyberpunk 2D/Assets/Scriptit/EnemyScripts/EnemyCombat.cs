using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public GameObject ammoSpawn; 

    public Transform player;

    public bool playerInRange; 

    public float maxCounter;
    public float counter;
    public float detectionRange; 
    public GameObject projectile;
    public AudioSource Lasegun;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
    }

    // Update is called once per frame
    void Update()
    {

        if(counter > maxCounter && playerInRange)
        {
            Shoot();
            counter = 0;
            Lasegun.Play();
        }
        else
        {
            counter += Time.deltaTime;
        }   

        if(Vector2.Distance(transform.position, player.transform.position) < detectionRange){
            
            playerInRange = true;
        }else
        {
         

            playerInRange = false; 
        }
    }
    /*
    c
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;

        }
    }
    */
    private void Shoot()
    {
        GameObject proj =  Instantiate(projectile, ammoSpawn.transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().shootDirection = transform.localScale.x*-1;


    }
}
