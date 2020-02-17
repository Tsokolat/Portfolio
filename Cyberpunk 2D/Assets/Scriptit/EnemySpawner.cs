using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] spawnPoints;
    public Transform[] waypoints;
    public float counter;
    public float maxCounter;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (counter > maxCounter)
        {
            int rnd = Mathf.RoundToInt(Random.Range(0, spawnPoints.Length));
            Debug.Log(rnd);
            GameObject enm =  Instantiate(enemy, spawnPoints[rnd].transform.position, Quaternion.identity);
            enm.GetComponent<EnemyMovement>().waypoints[0] = waypoints[Mathf.RoundToInt(Random.Range(0, waypoints.Length))];
            enm.GetComponent<EnemyMovement>().waypoints[1] = waypoints[Mathf.RoundToInt(Random.Range(0, waypoints.Length))];
            enm.GetComponent<EnemyMovement>().target = enm.GetComponent<EnemyMovement>().waypoints[0];

           
            counter = 0;

        }
        else
        {
            counter += Time.deltaTime;
        }

    }
}
