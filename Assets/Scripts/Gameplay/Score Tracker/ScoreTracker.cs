using System;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static Action<int> CurrentScoreChanged;

    [Header("Components")]
    [SerializeField]
    private ShapeData allShapeData;

    private int currentScore;

    private void Start()
    {
        OnGameStarted();
    }

    private void OnGameStarted()
    {
        currentScore = 0;
        ShapesGoal.ShapeEntered += OnShapeEnteredGoal;
    }

    private void OnGameStopped()
    {
        ShapesGoal.ShapeEntered -= OnShapeEnteredGoal;
    }

    private void OnShapeEnteredGoal(ShapesGoal goal, Shape shape)
    {
        currentScore += shape.ShapeDatum.Score;
        CurrentScoreChanged?.Invoke(currentScore);
    }
}

