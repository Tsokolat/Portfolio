using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Game;

public class MaxLevelMergeMediator 
{
    private ScoreTracker scoreTracker;
    
    public MaxLevelMergeMediator(ScoreTracker scoreTracker)
    {
        this.scoreTracker = scoreTracker;
    }

    public void Mediate()
    {
        scoreTracker.IncrementScore();
    }
}
