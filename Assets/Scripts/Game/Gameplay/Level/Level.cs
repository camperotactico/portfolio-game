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
    [SerializeField]
    private GameTimerEventChannel gameTimerEventChannel;
    [SerializeField]
    private ScoreTrackerEventChannel scoreTrackerEventChannel;

    private LevelDatum levelDatum;


    public void Start()
    {
        Debug.Log("TODO: Move this from here");
        Application.targetFrameRate = 0;
        levelDatum = levelLoadRequestRuntimeSet.LevelDatum;

        gameTimerEventChannel.Stopped.AddListener(OnGameTimerStoppedOrTimedOut);
        gameTimerEventChannel.Timeout.AddListener(OnGameTimerStoppedOrTimedOut);

        LevelInitialisationRequested.Emit(levelDatum);
        LevelStarted.Emit();
    }

    private void OnGameTimerStoppedOrTimedOut()
    {
        gameTimerEventChannel.Stopped.RemoveListener(OnGameTimerStoppedOrTimedOut);
        gameTimerEventChannel.Timeout.RemoveListener(OnGameTimerStoppedOrTimedOut);


        scoreTrackerEventChannel.LevelScoringFinished.AddListener(OnLevelScoringFinished);
        LevelFinished.Emit();
    }

    private void OnLevelScoringFinished(int finishScore)
    {
        scoreTrackerEventChannel.LevelScoringFinished.RemoveListener(OnLevelScoringFinished);

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