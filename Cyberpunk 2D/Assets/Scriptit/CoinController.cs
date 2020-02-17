using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioSource Coindrop;

    bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !collected)
        {
            other.gameObject.GetComponent<PlayerInventory>().AddCoins();
            Coindrop.Play();
            Destroy(gameObject.transform.root.gameObject);
            collected = true;
        }


    }
}
