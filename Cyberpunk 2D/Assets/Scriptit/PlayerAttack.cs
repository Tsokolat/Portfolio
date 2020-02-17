using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //private bool attacking = false;
    //private float attackTimer = 0;
    //private float attackCd = 0.3f;

    public Collider2D AttackTrigger;
    private Animator anim;
    public AudioSource Sword_two;
    

     void Start()
    {
         
    }

     void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        AttackTrigger.enabled = false;
    }

   
    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Sword_two.Play();
            anim.SetTrigger("Attack2");
            AttackTrigger.enabled = true;
           
        }
        else if (Input.GetKeyDown("e"))
        {
            Sword_two.Play();
            anim.SetTrigger("Attack3");
            AttackTrigger.enabled = true;
           
        }
        if (Input.GetKeyDown("q"))
        {
            Sword_two.Play();
            anim.SetTrigger("Attack4");
            AttackTrigger.enabled = true;
           
        }
        else
        {
            //HideTrigger();

        }


    }

    public void HideTrigger()
    {
        AttackTrigger.enabled = false;

    }
    

}



