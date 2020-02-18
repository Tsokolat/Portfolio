using System;
using UnityEngine;


[Serializable]
public class Combination
{

    public event Action<int> LevelChanged = delegate {};
    public event Action<Vector2Int> PositionChanged = delegate {};

    private int level = 0;
    private Vector2Int currentPosition;



    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            LevelChanged(value);

        }

    }
    public Vector2Int CurrentPosition
    {
        get
        {
            return currentPosition;
        }
        set
        {
            currentPosition = value;
            PositionChanged(value);
        }
    }

}

