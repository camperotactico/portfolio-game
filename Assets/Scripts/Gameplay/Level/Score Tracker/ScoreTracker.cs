using System;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static Action<int> CurrentScoreChanged;

    [Header("Components")]
    [SerializeField]
    private ShapeData allShapeData;


    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;
    [SerializeField]
    private ShapeLifecycleEventChannel shapeLifecycleEventChannel;

    private int currentScore;


    void OnEnable()
    {
        levelLifecycleEventChannel.InitialisationRequested.AddListener(OnLevelInitialisationRequested);
        levelLifecycleEventChannel.Started.AddListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.AddListener(OnLevelFinished);
    }
    void OnDisable()
    {
        levelLifecycleEventChannel.InitialisationRequested.RemoveListener(OnLevelInitialisationRequested);
        levelLifecycleEventChannel.Started.RemoveListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.RemoveListener(OnLevelFinished);
        shapeLifecycleEventChannel.EnteredGoal.RemoveListener(OnShapeEnteredGoal);
    }

    private void OnLevelInitialisationRequested(LevelDatum levelDatum)
    {

    }

    private void OnLevelStarted()
    {
        currentScore = 0;
        shapeLifecycleEventChannel.EnteredGoal.AddListener(OnShapeEnteredGoal);
    }

    private void OnLevelFinished()
    {
        shapeLifecycleEventChannel.EnteredGoal.RemoveListener(OnShapeEnteredGoal);
    }

    private void OnShapeEnteredGoal(Shape shape, ShapesGoal shapesGoal)
    {
        currentScore += shape.ShapeDatum.Score;
        CurrentScoreChanged?.Invoke(currentScore);
    }
}

