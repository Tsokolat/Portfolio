using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPresenter : Presenter<BoardMediator> 
{
    
    [HideInInspector] public GameObject[,] GameBoard = new GameObject[0,0];
    // Shows in IDE , but not in Unity Editor.

    public override void OnAwake()
    {
        
    }
}





