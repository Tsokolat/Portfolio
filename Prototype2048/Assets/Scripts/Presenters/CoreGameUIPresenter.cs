using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameUIPresenter : Presenter<CoreGameUIMediator>
{
    [SerializeField] // Hidden in IDE, but visible in Unity Editor
    private GameObject GameOverUIPanel;

    public void DisplayGameOverUIPanel()
    {
        GameOverUIPanel.SetActive(true);
    }
 
}
