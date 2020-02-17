using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class Item_Gun : ScriptableObject
{
    new public string name = "Gun";
    public Sprite fun = null;

    public virtual void Use()
    {

    }
}
