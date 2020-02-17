using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Pickup : Interactables
{
    public Item_Gun gun;
    PlayerController playerContr;

    public override void Interact()
    {
        base.Interact();
        Pickup();
    }

    void Pickup()
    {
        gun.Use();
        Destroy(gameObject);
    }
}
