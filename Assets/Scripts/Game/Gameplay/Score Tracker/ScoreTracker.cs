using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [Header("Emitting Event Channels")]
    [SerializeField]
    private ScoreTrackerEventChannel scoreTrackerEventChannel;

    [Header("Receiving Event Channels")]
    public LevelDatumEventChannel LevelInitialisationRequested;
    public VoidEventChannel LevelStarted;
    public VoidEventChannel LevelFinished;
    [SerializeField]
    private ShapeLifecycleEventChannel shapeLifecycleEventChannel;

    private int completionScore;
    private int currentScore;


    void OnEnable()
    {
        LevelInitialisationRequested.AddListener(OnLevelInitialisationRequested);
        LevelStarted.AddListener(OnLevelStarted);
        LevelFinished.AddListener(OnLevelFinished);
    }
    void OnDisable()
    {
        LevelInitialisationRequested.RemoveListener(OnLevelInitialisationRequested);
        LevelStarted.RemoveListener(OnLevelStarted);
        LevelFinished.RemoveListener(OnLevelFinished);
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

