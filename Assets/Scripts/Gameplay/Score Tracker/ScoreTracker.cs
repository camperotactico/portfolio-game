using System;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private ShapeData allShapeData;

    [Header("Emitting Event Channels")]
    [SerializeField]
    private ScoreTrackerEventChannel scoreTrackerEventChannel;

    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;
    [SerializeField]
    private ShapeLifecycleEventChannel shapeLifecycleEventChannel;

    private int completionScore;
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
        completionScore = levelDatum.CompletionScore;
        scoreTrackerEventChannel.EmitCompletionScoreChanged(completionScore);
    }

    private void OnLevelStarted()
    {
        currentScore = 0;
        shapeLifecycleEventChannel.EnteredGoal.AddListener(OnShapeEnteredGoal);
    }

    private void OnLevelFinished()
    {
        shapeLifecycleEventChannel.EnteredGoal.RemoveListener(OnShapeEnteredGoal);
        scoreTrackerEventChannel.EmitLevelScoringFinished(currentScore);
    }

    private void OnShapeEnteredGoal(Shape shape, ShapesGoal shapesGoal)
    {
        currentScore += shape.ShapeDatum.Score;
        scoreTrackerEventChannel.EmitCurrentScoreChanged(currentScore);
    }
}

