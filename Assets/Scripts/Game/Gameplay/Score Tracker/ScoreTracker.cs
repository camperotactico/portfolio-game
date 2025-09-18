using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [Header("Emitting Event Channels")]
    public IntEventChannel CompletionScoreChanged;
    public IntEventChannel CurrentScoreChanged;
    public IntEventChannel LevelScoringFinished;

    [Header("Receiving Event Channels")]
    public LevelDatumEventChannel LevelInitialisationRequested;
    public VoidEventChannel LevelStarted;
    public VoidEventChannel LevelFinished;
    public ShapeShapesGoalEventChannel ShapeEnteredShapesGoal;

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
        ShapeEnteredShapesGoal.RemoveListener(OnShapeEnteredGoal);
    }


    private void OnLevelInitialisationRequested(LevelDatum levelDatum)
    {
        completionScore = levelDatum.CompletionScore;
        CompletionScoreChanged.Emit(completionScore);
    }

    private void OnLevelStarted()
    {
        currentScore = 0;
        ShapeEnteredShapesGoal.AddListener(OnShapeEnteredGoal);
    }

    private void OnLevelFinished()
    {
        ShapeEnteredShapesGoal.RemoveListener(OnShapeEnteredGoal);
        LevelScoringFinished.Emit(currentScore);
    }

    private void OnShapeEnteredGoal(Shape shape, ShapesGoal shapesGoal)
    {
        currentScore += shape.ShapeDatum.Score;
        CurrentScoreChanged.Emit(currentScore);
    }
}

