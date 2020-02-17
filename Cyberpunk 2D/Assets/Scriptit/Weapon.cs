using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool hasWeapon;
    Animator animator;
    public AudioSource pistol;

   
    public void Update()
    {
        


        if (Input.GetButtonDown("Fire2") && hasWeapon)//&& animator.GetBool("Armed"))
        {
            pistol.Play();
            Shoot();
        }
        
          
    }
    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    
}