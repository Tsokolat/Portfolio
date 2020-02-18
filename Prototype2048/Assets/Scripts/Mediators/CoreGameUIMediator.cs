using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoreGameUIMediator : Mediator<CoreGameUIPresenter>
{
    private Board Board;
    public override void Mediate()
    {
        Board = ServiceLocator.Resolve<Board>();

        // Mediates GameEnded event to Presenter.
        Board.GameEnded += Presenter.DisplayGameOverUIPanel;
        
        
    }
 

}
