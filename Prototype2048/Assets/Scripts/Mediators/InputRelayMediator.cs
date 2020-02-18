using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model;
using UnityEngine;

public class InputRelayMediator : Mediator<InputPresenter>
{
    private Board Board;

    public override void Mediate()
    {
        Board = ServiceLocator.Resolve<Board>();

        Presenter.KeySpacePressed += OnKeySpacePressed;
        Presenter.KeyAPressed += OnKeyAPressed;
        Presenter.KeyDPressed += OnKeyDPressed;
        Presenter.KeySPressed += OnKeySPressed;
        Presenter.KeyWPressed += OnKeyWPressed;
        Presenter.Swiped += OnSwiped;

        Presenter.KeyPPressed += OnKeyPPressed;






    }

    private void OnKeyPPressed()
    {
        Time.timeScale = 0;
        
    }

    private void OnSwipeAction(Vector2 dirVector)
    {
        //board.MakeMove(dirVector);
    }
    private void OnKeySpacePressed()
    {


         // Board.SpawnCombinationToRandomLocation();
        //remember to uncomment for game to work
        Board.AddCombination(new Combination() { Level = 1, CurrentPosition = new Vector2Int(0, 0) });
        Board.AddCombination(new Combination() { Level = 2, CurrentPosition = new Vector2Int(1, 0) });
        Board.AddCombination(new Combination() { Level = 3, CurrentPosition = new Vector2Int(2, 0) });
        Board.AddCombination(new Combination() { Level = 4, CurrentPosition = new Vector2Int(3, 0) });
                                                                                         
        Board.AddCombination(new Combination() { Level = 4, CurrentPosition = new Vector2Int(0, 1) });
        Board.AddCombination(new Combination() { Level = 3, CurrentPosition = new Vector2Int(1, 1) });
        Board.AddCombination(new Combination() { Level = 2, CurrentPosition = new Vector2Int(2, 1) });
        Board.AddCombination(new Combination() { Level = 1, CurrentPosition = new Vector2Int(3, 1) });
                                                                                         
        Board.AddCombination(new Combination() { Level = 1, CurrentPosition = new Vector2Int(0, 2) });
        Board.AddCombination(new Combination() { Level = 2, CurrentPosition = new Vector2Int(1, 2) });
        Board.AddCombination(new Combination() { Level = 3, CurrentPosition = new Vector2Int(2, 2) });
        Board.AddCombination(new Combination() { Level = 4, CurrentPosition = new Vector2Int(3, 2) });
                                                                                         
        Board.AddCombination(new Combination() { Level = 4, CurrentPosition = new Vector2Int(0, 3) });
        Board.AddCombination(new Combination() { Level = 3, CurrentPosition = new Vector2Int(1, 3) });
        Board.AddCombination(new Combination() { Level = 2, CurrentPosition = new Vector2Int(2, 3) });
        Board.AddCombination(new Combination() { Level = 1, CurrentPosition = new Vector2Int(3, 3) });
                                                                                         
                                                                                         
                                                                                         





    }

    private void OnKeyAPressed()
    {
        Board.MoveHorizontal(Vector2Int.left);
    }

    private void OnKeyDPressed()
    {

        Board.MoveHorizontal(Vector2Int.right);
    }

    private void OnKeySPressed()
    {

        Board.MoveVertical(Vector2Int.down);
    }

    private void OnKeyWPressed()
    {
        Board.MoveVertical(Vector2Int.up);
    }
    private void OnSwiped(Vector2 direction)
    {
        if (direction.x > 0)
        {
            Board.MoveHorizontal(Vector2Int.right);
        }
        else if (direction.x < 0)
        {
            Board.MoveHorizontal(Vector2Int.left);
        }

        else if (direction.y > 0)
        {
            Board.MoveVertical(Vector2Int.up);
        }
        else if (direction.y < 0)
        {
            Board.MoveVertical(Vector2Int.down);
        }
    }

}
