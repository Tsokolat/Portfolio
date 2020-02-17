using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
        public int MaxHealth;
        public int CurrentHealth;

    public GameObject DropGun;

    public GameObject Enemyblood;

    void Start()
    {
        CurrentHealth = MaxHealth;
     
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(DropGun, transform.position, DropGun.transform.rotation);
        }
    }

    public void AddDamage(int damageToGive)
    {
        GameStatus.status.RunShake(0.5f);
        CurrentHealth -= damageToGive;
        Instantiate(Enemyblood, transform.position, Quaternion.identity);
    }

    public void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
