using Assets.Scripts.Model;
using UnityEngine;
using Assets.Scripts.Game;
using System.Collections.Generic;
using System;

public class BoardMediator : Mediator<BoardPresenter>
{
    private Board Board;
    public GameObject BoardPiecePrototype;
    private Vector3 BoardPieceDimensions;
    private Vector3 HalfPieceDimensions;
    private Vector3 CombinationDimensions;
    private readonly List<CombinationPiecePresenter> CombinationPresenters = new List<CombinationPiecePresenter>();

    public GameObject CombinationPiecePrototype;

    private float RemainingTime;


    public override void Mediate()
    {
        BoardPieceDimensions = BoardPiecePrototype.GetComponent<MeshRenderer>().bounds.size;
        HalfPieceDimensions = BoardPieceDimensions / 2;
        Board = ServiceLocator.Resolve<Board>();
        Board.SizeChanged += OnBoardSizeChanged;
        OnBoardSizeChanged(Board.Width, Board.Height);

        Board.CombinationAdded += OnCombinationAdded;
        CombinationDimensions = CombinationPiecePrototype.GetComponentInChildren<MeshRenderer>().bounds.size;

        Board.CombinationRemoved += OnCombinationRemoved;

        Board.TransitionProgressedChanged += OnTransitionProgressChanged;
    }



    public void Start()
    {
         Board.SpawnCombinationToRandomLocation();
        // Remember to remove for game to function normally
    }

    public void Update()
    {
        if (Board.TransitionInProgress)
        {
            RemainingTime = SubstractTime(RemainingTime, Time.deltaTime);
            Board.TransitionInProgress = IsTransitionStillInProgress(RemainingTime);
        }
    }


    private void OnTransitionProgressChanged(bool transitionInProgress)
    {
        if (transitionInProgress)
        {
            RemainingTime = Board.TransitionTime;
        }
        else
        {
            RemainingTime = 0;
            Board.HandleTransitionComplete();
        }
    }

    private static float SubstractTime(float remainingTime, float deltaTime)
    {
        return remainingTime -= deltaTime;
    }
    private static bool IsTransitionStillInProgress(float remainingTime)
    {
        return remainingTime > 0;
    }

    private void OnCombinationRemoved(Combination combination)
    {
        var combinationCount = CombinationPresenters.Count;
        for (int i = 0; i < CombinationPresenters.Count; i++)
        {
            if (CombinationPresenters[i].Position == combination.CurrentPosition)
            {
                Remove(CombinationPresenters[i].gameObject);
                CombinationPresenters.RemoveAt(i);
                break;
            }

        }
        if (combinationCount == CombinationPresenters.Count)
        {
            throw new Exception($"Could not remove combination at position {combination.CurrentPosition}");
        }
    }

    private void OnCombinationAdded(Combination combination)
    {
        // Create necessary objects
        var combinationPos = combination.CurrentPosition;
        var combinationGameObject = CreatePiece(CombinationPiecePrototype);
        var combinationPresenter = combinationGameObject.GetComponent<CombinationPiecePresenter>();

        // Set initial values
        combinationGameObject.transform.position = new Vector3(combinationPos.x * BoardPieceDimensions.x, HalfPieceDimensions.y + CombinationDimensions.y / 2, combinationPos.y * BoardPieceDimensions.z);
        combinationPresenter.OnBoardPositionChanged(combination.CurrentPosition);
        combinationPresenter.OnTransitionTimeChanged(Board.TransitionTime);

        // add to presenters
        CombinationPresenters.Add(combinationPresenter);

        // Subscribe to events
        combination.PositionChanged += position => OnCombinationPositionChanged(position, combinationPresenter);
        combination.LevelChanged += level => OnCombinationLevelChanged(level, combinationPresenter);


        OnCombinationLevelChanged(combination.Level, combinationPresenter);
    }

    private void OnBoardSizeChanged(int width, int height)
    {
        for (int x = 0; x < Presenter.GameBoard.GetLength(0); x++)
        {
            for (int y = 0; y < Presenter.GameBoard.GetLength(1); y++)
            {
                Remove(Presenter.GameBoard[x, y]);
            }
        }

        Presenter.GameBoard = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var piece = CreatePiece(BoardPiecePrototype);
                piece.transform.position = new Vector3(x * BoardPieceDimensions.x, 0, y * BoardPieceDimensions.y);
                Presenter.GameBoard[x, y] = piece;
            }
        }
    }



    private GameObject CreatePiece(GameObject prototype)
    {
        return Instantiate(prototype);
    }

    private void Remove(GameObject prototype)
    {
        Destroy(prototype);
    }


    private void OnCombinationPositionChanged(Vector2Int combinationPos, CombinationPiecePresenter combinationPresenter)
    {
        combinationPresenter.OnPositionChanged(new Vector3(combinationPos.x * BoardPieceDimensions.x, HalfPieceDimensions.y + CombinationDimensions.y / 2, combinationPos.y * BoardPieceDimensions.z));
        combinationPresenter.OnBoardPositionChanged(combinationPos);
    }

    private void OnCombinationLevelChanged(int pieceLevel, CombinationPiecePresenter combinationPresenter)
    {
        combinationPresenter.OnLevelChange(pieceLevel);

    }
}
