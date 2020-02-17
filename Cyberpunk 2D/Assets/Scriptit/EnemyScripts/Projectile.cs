using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    public float shootDirection;

    public GameObject blood;
    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
        */
        transform.Translate(shootDirection * Time.deltaTime * speed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(blood, transform.position, Quaternion.identity);

            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    
}
