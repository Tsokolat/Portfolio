using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int level;

    public CombinationSpawner[,] LevelObjects;
    public CombinationSpawner(int level , GameObject Prefab)
    {
        this.level = level;
        this.prefab = Prefab;
    }

}
