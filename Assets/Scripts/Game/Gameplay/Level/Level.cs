using Game.Scriptable_Objects.Data.Levels;
using UnityEngine;

public class Level : MonoBehaviour
{

    [Header("Runtime Sets")]
    public LevelLoadRequestRuntimeSet LevelLoadRequestRuntimeSet;

    [Header("Components")]
    public SceneReference MainMenuSceneReference;
    public SceneReference GameplaySceneReference;

    [Header("Emitting Event Channels")]
    public LevelDatumEventChannel LevelInitialisationRequested;
    public VoidEventChannel LevelStarted;
    public VoidEventChannel LevelFinished;
    public VoidEventChannel LevelCompleted;
    public VoidEventChannel LevelFailed;


    [Header("Receiving Event Channels")]
    public VoidEventChannel GameTimerStopped;
    public VoidEventChannel GameTimerTimedOut;
    public IntEventChannel ScoreTrackerLevelScoringFinished;
    public VoidEventChannel PlayNextLevelButtonPressed;

    private LevelDatum levelDatum;

    void OnEnable()
    {
        GameTimerStopped.AddListener(OnGameTimerStoppedOrTimedOut);
        GameTimerTimedOut.AddListener(OnGameTimerStoppedOrTimedOut);
        ScoreTrackerLevelScoringFinished.AddListener(OnLevelScoringFinished);

    }

    void OnDisable()
    {
        GameTimerStopped.RemoveListener(OnGameTimerStoppedOrTimedOut);
        GameTimerTimedOut.RemoveListener(OnGameTimerStoppedOrTimedOut);
        ScoreTrackerLevelScoringFinished.RemoveListener(OnLevelScoringFinished);
    }

    public void Start()
    {
        Debug.Log("TODO: Move this from here");
        Application.targetFrameRate = 0;
        levelDatum = LevelLoadRequestRuntimeSet.GetLevelDatum();

        LevelInitialisationRequested.Emit(levelDatum);
        LevelStarted.Emit();
    }

    private void OnGameTimerStoppedOrTimedOut()
    {
        LevelFinished.Emit();
    }

    private void OnLevelScoringFinished(int finishScore)
    {
        if (finishScore < levelDatum.CompletionScore)
        {
            LevelFailed.Emit();
        }
        else
        {
            LevelCompleted.Emit();
        }
    }


}