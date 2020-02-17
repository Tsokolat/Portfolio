using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchColliderControl : MonoBehaviour
{
    public CapsuleCollider2D stand;
    public CapsuleCollider2D crouch;

    PlayerController playerC;
    void Start()
    {
        playerC = GetComponent<PlayerController>();
        stand.enabled = true;
        crouch.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerC.grounded == false)
        {
            stand.enabled = true;
            crouch.enabled = false;
        }
        else
        {
            if(playerC.crouching == true)
            {
                stand.enabled = false;
                crouch.enabled = true;
            }
            else
            {
                stand.enabled = true;
                crouch.enabled = false;
            }
        }
           
    }
}
