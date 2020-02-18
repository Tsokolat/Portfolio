using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Game;
using System;
using TMPro;

public class ScoreTrackerPresenter : Presenter<ScoreTrackerMediator>
{
    public TextMeshProUGUI TextField;
    public void OnScoreChanged(int score)
    {
        TextField.SetText(score.ToString());
    }
}
