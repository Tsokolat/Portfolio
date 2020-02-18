using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Game;
using System;

public class ScoreTrackerMediator : Mediator<ScoreTrackerPresenter>
{
    private ScoreTracker score;
    public override void Mediate()
    {
        score = ServiceLocator.Resolve<ScoreTracker>();
        OnScoreChanged(score.Score);
        score.ScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        Presenter.OnScoreChanged(score);
    }
}
