using UnityEngine;

[RequireComponent(typeof(GameTimer))]
public class LevelGameTimer : MonoBehaviour
{
    [SerializeField]
    private GameTimer gameTimer;

    [Header("Receiving Event Channels")]
    public LevelDatumEventChannel LevelInitialisationRequested;
    public VoidEventChannel LevelStarted;
    public VoidEventChannel LevelFinished;

    private float levelStartingTime;

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
    }


    private void OnLevelInitialisationRequested(LevelDatum levelDatum)
    {
        levelStartingTime = levelDatum.StartingTime;
    }
    private void OnLevelStarted()
    {
        gameTimer.Begin(levelStartingTime);
    }

    private void OnLevelFinished()
    {
        gameTimer.Stop();
    }


}
