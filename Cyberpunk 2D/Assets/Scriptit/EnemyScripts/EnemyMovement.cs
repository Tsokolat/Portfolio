using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform target;
    public Transform[] waypoints;
    public float speed;

    private int _index;

    private int index
    {
        get
        {
            if (_index == waypoints.Length)
            {
                _index = 0;
                return index;
            }
            else return _index++;
        }
    }

    private void OnEnable()
    {
        target = waypoints[0];
        if (target.position.x > transform.position.x)
            transform.localScale = new Vector2(-1, 1);
        else
            transform.localScale = Vector2.one;
    }

    private void Update()
    {
        if (GetComponent<EnemyCombat>().playerInRange == false)
        {
            speed = 5;
            if (Vector2.Distance(transform.position, target.position) > 0.02f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                target = waypoints[index];
                if (target.position.x > transform.position.x)
                    transform.localScale = new Vector2(-1, 1);
                else
                    transform.localScale = Vector2.one;
            }
        }
        else
        {
            speed = 0;
            if(transform.position.x < GetComponent<EnemyCombat>().player.transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
        }
    }
   
}
