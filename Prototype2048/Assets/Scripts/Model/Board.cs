using System;
using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;


namespace Assets.Scripts.Model
{
    [Serializable]
    public class Board
    {
        /* public delegate void MoveCountChangedHandler(int count);
         public event MoveCountChangedHandler ASD;

         public event EventHandler onSometghinChanged;
         */

        public Board(MaxLevelMergeMediator maxLevelMergeMediator)
        {
            Positions = new Combination[width, height];
            FreeCoordinates = new List<Vector2Int>(Positions.Length);
            this.maxLevelMergeMediator = maxLevelMergeMediator;
        }

        public event Action<int> OnMoveCountChanged = delegate { };
        public event Action<int[,]> BoardSizeChanged = delegate { };
        public event Action<int, Vector2Int> BoardPieceChanged = delegate { };
        public event Action<int, int> SizeChanged = delegate { };
        public event Action<Combination> CombinationAdded = delegate { };
        public event Action<Combination> CombinationRemoved = delegate { };
        public event Action<Vector2Int> CombinationMoved = delegate { };
        public event Action<bool> TransitionProgressedChanged = delegate { };
        public event Action<float> TransitionTimeChanged = delegate { };
        public event Action GameEnded = delegate { };

        Animator myAnim;

     //   private int moveCounter = 0;
        private int CombinationMaxLevel = 5;

        private int width = 4;
        private int height = 4;

        private Combination[,] Positions;
        private List<Vector2Int> FreeCoordinates;

        private Random Random = new Random();

        private readonly MaxLevelMergeMediator maxLevelMergeMediator;

        private bool transitionInProgress;
        private float transitionTime = 0.15f;

        public float TransitionTime
        {
            get
            {
                return transitionTime;
            }
            set
            {
                transitionTime = value;
                TransitionTimeChanged(transitionTime);

            }
        }
        public bool TransitionInProgress
        {
            get
            {
                return transitionInProgress;
            }
            set
            {
                SetWithNotify(ref transitionInProgress, value, TransitionProgressedChanged);
            }
        }


        private void SetWithNotify<T>(ref T variable, T newValue, Action<T> onChange)
        {
            if (!EqualityComparer<T>.Default.Equals(variable, newValue))
            {
                variable = newValue;
                onChange(variable);
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                SizeChanged(width, height);
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                SizeChanged(width, height);
            }
        }



        public void SpawnCombinationToRandomLocation()
        {
            UpdateFreeCordinates();
            var randomCordinate = GetRandomCordinate();
            var combination = new Combination()
            {
                Level = 1,
                CurrentPosition = randomCordinate
            };

            AddCombination(combination);

        }



        public void UpdateFreeCordinates()
        {
            UpdateFreeCordinates(Positions, FreeCoordinates);


        }
        public void UpdateFreeCordinates(Combination[,] positions, List<Vector2Int> coordinates)
        {
            if (positions == null)
            {
                throw new NullReferenceException("given argument 'positions' was null");
            }
            if (coordinates == null)
            {
                throw new NullReferenceException("given argument 'coordinates' was null");
            }

            coordinates.Clear();
            for (int x = 0; x < positions.GetLength(0); x++)
            {
                for (int y = 0; y < positions.GetLength(1); y++)
                {

                    if (positions[x, y] == null)
                    {
                        coordinates.Add(new Vector2Int(x, y));
                    }
                }
            }
        }

        private Vector2Int GetRandomCordinate()
        {
            return GetRandomCordinate(FreeCoordinates);
        }
        public Vector2Int GetRandomCordinate(List<Vector2Int> freeCoordinates)
        {
            if (freeCoordinates == null)
            {
                throw new NullReferenceException($"Given Argument {nameof(freeCoordinates)} was null");
            }
            if (freeCoordinates.Count <= 0)
            {
                throw new Exception($"Given Argument {nameof(freeCoordinates)} list was empty'");
            }
            int randomIndex = Random.Next(freeCoordinates.Count);
            return freeCoordinates[randomIndex];
        }





        public void AddCombination(Combination combination)
        {

            if (Positions[combination.CurrentPosition.x, combination.CurrentPosition.y] != null)
            {
                throw new Exception($"Cannot add a combination to position {combination.CurrentPosition} because it already has a combination assigned");
            }

            Positions[combination.CurrentPosition.x, combination.CurrentPosition.y] = combination;
            CombinationAdded(combination);
        }
        public void RemoveCombination(Combination combination)
        {
            CombinationRemoved(combination);
            Positions[combination.CurrentPosition.x, combination.CurrentPosition.y] = null;
        }

        public void MoveHorizontal(Vector2Int direction)
        {
            if (TransitionInProgress)
            {
                return;
            }
            for (int y = 0; y < height; y++)
            {
                MoveHorizontalRow(direction, y);
            }
        }

        public void MoveVertical(Vector2Int direction)
        {
            if (TransitionInProgress)
            {
                return;
            }
            for (int x = 0; x < width; x++)
            {
                MoveVerticalRow(direction, x);
            }
        }

        private void MoveHorizontalRow(Vector2Int direction, int y)
        {
            // Condition is true when Swipe is left.
            bool isLeftSwipe = (direction.x < 0);
            int startIndex = isLeftSwipe ? 0 : width - 1;
            int endIndex = isLeftSwipe ? width - 1 : 0;
            var mergeLimit = startIndex + direction.x;
            // This loops iterates always the opposite of given input
            for (int i = startIndex; i != endIndex - direction.x; i -= direction.x)
            {
                // This loop iterates to reversed direction of outer loop untill mergelimit has been reached.
                for (int j = i; j != mergeLimit; j += direction.x)
                {
                    var currentCell = new Vector2Int(j, y);
                    var adjecentCell = new Vector2Int(j + direction.x, y);

                    if (CanMove(currentCell, adjecentCell))
                    {
                        TransitionInProgress = true;
                        if (CausesMerge(currentCell, adjecentCell))
                        {
                            var currentLevel = Positions[currentCell.x, currentCell.y].Level;
                            Merge(currentCell, adjecentCell);
                            
                            if (currentLevel != CombinationMaxLevel)
                            {
                                mergeLimit = j;
                            }
                            break;
                        }
                        Move(currentCell, adjecentCell);
                    }

                }
            }
        }

        private void MoveVerticalRow(Vector2Int direction, int x)
        {

            bool isDownSwipe = (direction.y < 0);
            int startIndex = isDownSwipe ? 0 : height - 1;
            int endIndex = isDownSwipe ? height - 1 : 0;
            var mergeLimit = startIndex + direction.y;

            for (int i = startIndex; i != endIndex - direction.y; i -= direction.y)
            {
                for (int j = i; j != mergeLimit; j += direction.y)
                {
                    var currentCell = new Vector2Int(x, j);
                    var adjecentCell = new Vector2Int(x, j + direction.y);
                    if (CanMove(currentCell, adjecentCell))
                    {
                        TransitionInProgress = true;
                        if (CausesMerge(currentCell, adjecentCell))
                        {
                            var currentLevel = Positions[currentCell.x, currentCell.y].Level;
                            Merge(currentCell, adjecentCell);
                            
                            if (currentLevel != CombinationMaxLevel)
                            {
                                mergeLimit = j;
                            }
                            break;
                        }
                        Move(currentCell, adjecentCell);
                    }
                }
            }
        }

        private void Move(Vector2Int currentCell, Vector2Int adjecentCell)
        {
            var combination = Positions[currentCell.x, currentCell.y];
            Positions[adjecentCell.x, adjecentCell.y] = combination;
            combination.CurrentPosition = adjecentCell;
            Positions[currentCell.x, currentCell.y] = null;
        }

        private void Merge(Vector2Int currentCell, Vector2Int adjecentCell)
        {
            var currentCombination = Positions[currentCell.x, currentCell.y];

            RemoveCombination(currentCombination);
            var adjecentCombination = Positions[adjecentCell.x, adjecentCell.y];

            if (adjecentCombination.Level != CombinationMaxLevel)
            {
                adjecentCombination.Level++;
            }
            else
            {
                RemoveCombination(adjecentCombination);
                maxLevelMergeMediator.Mediate();
            }
        }

        private bool CausesMerge(Vector2Int currentCell, Vector2Int adjecentCell)
        {
            //TODO: Grid bounds safety. Bools from CanMove.
            var adjecentCombination = Positions[adjecentCell.x, adjecentCell.y];
            var currentCombination = Positions[currentCell.x, currentCell.y];
            if (adjecentCombination == null)
            {
                return false;
            }
            return currentCombination.Level == adjecentCombination.Level;
        }

        private bool CanMove(Vector2Int currentCell, Vector2Int adjecentCell)
        {
            bool isCurrentCellWithInWidth = currentCell.x < width && currentCell.x >= 0;
            bool isCurrentCellWithInHeight = currentCell.y < height && currentCell.y >= 0;
            bool isCurrentCellWithInGrid = isCurrentCellWithInWidth && isCurrentCellWithInHeight;
            if (!isCurrentCellWithInGrid)
            {
                return false;
            }

            var currentCombination = Positions[currentCell.x, currentCell.y];
            if (currentCombination == null)
            {
                return false;
            }

            bool isWithInWidth = adjecentCell.x < width && adjecentCell.x >= 0;
            bool isWithInHeight = adjecentCell.y < height && adjecentCell.y >= 0;
            bool isWithInGrid = isWithInWidth && isWithInHeight;
            if (!isWithInGrid)
            {
                return false;
            }

            var adjecentCombination = Positions[adjecentCell.x, adjecentCell.y];
            if (adjecentCombination == null)
            {
                return true;
            }
            // TODO: Validate that current combination is within grid.
            return currentCombination.Level == adjecentCombination.Level;
        }

        public void HandleTransitionComplete()
        {
            SpawnCombinationToRandomLocation();
            if (IsGameOver())
            {
                GameEnded();
            }
        }

        private bool IsGameOver()
        {
            for (int i = 0; i < Positions.GetLength(0); i++)
            {
                for (int j = 0; j < Positions.GetLength(1); j++)
                {
                    var currentCell = new Vector2Int(i, j);
                    var cellToLeft = new Vector2Int(i - 1, j);
                    var cellToRight = new Vector2Int(i + 1, j);
                    var cellToUp = new Vector2Int(i, j + 1);
                    var cellToDown = new Vector2Int(i, j - 1);

                    if (CanMove(currentCell, cellToLeft) ||
                        CanMove(currentCell, cellToRight) ||
                        CanMove(currentCell, cellToUp) ||
                        CanMove(currentCell, cellToDown))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


    }
}


