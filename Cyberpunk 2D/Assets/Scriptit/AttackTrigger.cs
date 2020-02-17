using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public int damageToGive;

    //public int dmg = 20;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.isTrigger!=true&&collision.CompareTag("Enemy"))
    //    {
    //        collision.SendMessageUpwards("Damage", dmg);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealth>().AddDamage(damageToGive);
        }
    }
}
