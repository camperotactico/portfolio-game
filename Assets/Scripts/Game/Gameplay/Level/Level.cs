using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [Header("Runtime Sets")]
    public LevelLoadRequestRuntimeSet LevelLoadRequestRuntimeSet;
    public AvailableLevelDataRuntimeSet AvailableLevelDataRuntimeSet;

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
    public VoidEventChannel ExitToMainMenuButtonPressed;
    public VoidEventChannel RetryLevelButtonPressed;
    public VoidEventChannel PlayNextLevelButtonPressed;

    private LevelDatum levelDatum;

    void OnEnable()
    {
        ExitToMainMenuButtonPressed.AddListener(OnExitToMainMenuButtonPressed);
        RetryLevelButtonPressed.AddListener(OnRetryLevelButtonPressed);
        PlayNextLevelButtonPressed.AddListener(OnPlayNextLevelButtonPressed);
        GameTimerStopped.AddListener(OnGameTimerStoppedOrTimedOut);
        GameTimerTimedOut.AddListener(OnGameTimerStoppedOrTimedOut);
        ScoreTrackerLevelScoringFinished.AddListener(OnLevelScoringFinished);

    }

    void OnDisable()
    {
        ExitToMainMenuButtonPressed.RemoveListener(OnExitToMainMenuButtonPressed);
        RetryLevelButtonPressed.RemoveListener(OnRetryLevelButtonPressed);
        PlayNextLevelButtonPressed.RemoveListener(OnPlayNextLevelButtonPressed);
        GameTimerStopped.RemoveListener(OnGameTimerStoppedOrTimedOut);
        GameTimerTimedOut.RemoveListener(OnGameTimerStoppedOrTimedOut);
        ScoreTrackerLevelScoringFinished.RemoveListener(OnLevelScoringFinished);
    }

    public void Start()
    {
        Debug.Log("TODO: Move this from here");
        Application.targetFrameRate = 0;
        levelDatum = LevelLoadRequestRuntimeSet.LevelDatum;

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

    private void OnExitToMainMenuButtonPressed()
    {
        SceneManager.LoadScene(MainMenuSceneReference.BuildIndex);
    }

    private void OnRetryLevelButtonPressed()
    {
        SceneManager.LoadScene(GameplaySceneReference.BuildIndex);
    }

    private void OnPlayNextLevelButtonPressed()
    {
        if (!AvailableLevelDataRuntimeSet.IsLoaded)
        {
            SceneManager.LoadScene(GameplaySceneReference.BuildIndex);
            return;
        }

        int nextLevelID = LevelLoadRequestRuntimeSet.LevelDatum.ID + 1;
        if (AvailableLevelDataRuntimeSet.TryGetLevelDatum(nextLevelID, out LevelDatum nextLevelDatum))
        {
            LevelLoadRequestRuntimeSet.LevelDatum = nextLevelDatum;
        }
        else if (AvailableLevelDataRuntimeSet.TryGetLevelDatum(1, out LevelDatum firstLevel))
        {
            LevelLoadRequestRuntimeSet.LevelDatum = firstLevel;
        }
        SceneManager.LoadScene(GameplaySceneReference.BuildIndex);
        return;

    }
}