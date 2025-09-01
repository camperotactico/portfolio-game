using System;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static Action<int> CurrentScoreChanged;

    [Header("Components")]
    [SerializeField]
    private ShapeData allShapeData;

    private int currentScore;


    public void OnLevelInitialisationRequested(LevelDatum levelDatum)
    {

    }

    public void OnLevelStarted()
    {
        currentScore = 0;
        ShapesGoal.ShapeEntered += OnShapeEnteredGoal;
    }

    public void OnLevelFinished()
    {
        ShapesGoal.ShapeEntered -= OnShapeEnteredGoal;
    }

    private void OnShapeEnteredGoal(ShapesGoal goal, Shape shape)
    {
        currentScore += shape.ShapeDatum.Score;
        CurrentScoreChanged?.Invoke(currentScore);
    }
}

