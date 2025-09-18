using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [Header("Level Parameters")]
    public LevelLoadRequestRuntimeSet levelLoadRequestRuntimeSet;

    [Header("Components")]
    [SerializeField]
    private SceneReference mainMenuSceneReference;

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

    private LevelDatum levelDatum;


    public void Start()
    {
        Debug.Log("TODO: Move this from here");
        Application.targetFrameRate = 0;
        levelDatum = levelLoadRequestRuntimeSet.LevelDatum;

        GameTimerStopped.AddListener(OnGameTimerStoppedOrTimedOut);
        GameTimerTimedOut.AddListener(OnGameTimerStoppedOrTimedOut);

        LevelInitialisationRequested.Emit(levelDatum);
        LevelStarted.Emit();
    }

    private void OnGameTimerStoppedOrTimedOut()
    {
        GameTimerStopped.RemoveListener(OnGameTimerStoppedOrTimedOut);
        GameTimerTimedOut.RemoveListener(OnGameTimerStoppedOrTimedOut);


        ScoreTrackerLevelScoringFinished.AddListener(OnLevelScoringFinished);
        LevelFinished.Emit();
    }

    private void OnLevelScoringFinished(int finishScore)
    {
        ScoreTrackerLevelScoringFinished.RemoveListener(OnLevelScoringFinished);

        if (finishScore < levelDatum.CompletionScore)
        {
            LevelFailed.Emit();
        }
        else
        {
            LevelCompleted.Emit();
        }

        // SceneManager.LoadScene(mainMenuSceneReference.BuildIndex);
    }
}