using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables: MonoBehaviour
{
    public bool interactable = false;
    public GameObject Gun;


    private void Start()
    {
        GetComponent<GameObject>();
    }

    public virtual void Interact()
    {
        Destroy(gameObject);
    }
    
    void Update()
    {
        if (interactable)
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            interactable = false;
        }
    }

}
