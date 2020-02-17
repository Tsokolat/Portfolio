using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthBonus : MonoBehaviour
{
    public float healthAmount;
    void Start()
    {
        transform.localScale = new Vector3(healthAmount / 5, healthAmount / 5, healthAmount / 5);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
