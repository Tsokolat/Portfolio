using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.Model
{
   
    [Serializable]
    public class CubeInstantiate : MonoBehaviour
    {
    
        public GameObject cubePrefab;
        public Vector2Int BoardSize;

        private int[,] CubeTransformArray = new int[,]
        {
            {0,0,0,0},
            {0,0,0,0},
            {0,0,0,0},
            {0,0,0,0},
            
        };

        public void PopulateArray()
        {
            
        }

        void Start()
        {
            for (int x = 0; x < BoardSize.x; x++)
            {

                for (int y = 0; y < BoardSize.y; y++)
                {
                    Instantiate(cubePrefab, new Vector3(x, 0, y), Quaternion.identity);
                }

            }

        }


    }

}










