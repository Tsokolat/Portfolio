using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationPiecePresenter : Presenter
{

    public Vector2Int Position { get; private set; }
    // [SerializeField] protected Color[] LevelColors;


    [SerializeField] protected GameObject[] LevelCombinations;




    //  [SerializeField] protected MeshRenderer CombinationMeshRenderer;






    //needed
    public float InterpolationTime;


    public void OnTransitionTimeChanged(float time)
    {
        InterpolationTime = time;
    }

    IEnumerator MoveToLerpCoroutine(Transform targetTransform, Vector3 targetPos, float interpolationTime)
    {

        var startPos = targetTransform.position;
        float elapsedTime = 0;

        while (elapsedTime < interpolationTime)
        {

            targetTransform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / interpolationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetTransform.position = targetPos;

    }


    public override void OnAwake()
    {

    }

    public void OnBoardPositionChanged(Vector2Int position)
    {
        // Debug.Log($"Ball {name} board position: {position}");
        Position = position;
    }

    public void OnPositionChanged(Vector3 position)
    {
        StartCoroutine(MoveToLerpCoroutine(transform, position, InterpolationTime));
    }

    public void OnLevelChange(int level)
    {
        //   CombinationMeshRenderer.material.SetColor("_Color", LevelColors[level]);

        if (LevelCombinations.Length < level)
        {
            throw new System.Exception($"LevelCombinations list length{LevelCombinations.Length} was smaller than level :{level}");
        }

        if(level != 1)
        {
            LevelCombinations[level - 2].SetActive(false);
        }
        LevelCombinations[level-1].SetActive(true);

        //if (level == 1)
        //{
        //    Level1Combination.SetActive(false);
        //    Level2Combination.SetActive(true);
        //}
        //if (level == 2)
        //{
        //    Level2Combination.SetActive(false);
        //    Level3Combination.SetActive(true);
        //}
        //if (level == 3)
        //{
        //    Level3Combination.SetActive(false);
        //    Level4Combination.SetActive(true);
        //}
        //if (level == 4)
        //{
        //    Level4Combination.SetActive(false);
        //    Level5Combination.SetActive(true);
        //}

    }
}
