using System;
using UnityEngine;

public class Level : MonoBehaviour
{

    [Header("Level Parameters")]
    public LevelDatum LevelDatum;

    [Header("Emitting Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;

    [Header("Receiving Event Channels")]
    [SerializeField]
    private GameTimerEventChannel gameTimerEventChannel;
    [SerializeField]
    private ScoreTrackerEventChannel scoreTrackerEventChannel;



    public void Start()
    {
        Debug.Log("TODO: Move this from here");
        Application.targetFrameRate = 0;

        gameTimerEventChannel.Stopped.AddListener(OnGameTimerStoppedOrTimedOut);
        gameTimerEventChannel.Timeout.AddListener(OnGameTimerStoppedOrTimedOut);

        levelLifecycleEventChannel.EmitInitialisationRequestedEvent(LevelDatum);
        levelLifecycleEventChannel.EmitStartedEvent();
    }

    private void OnGameTimerStoppedOrTimedOut()
    {
        gameTimerEventChannel.Stopped.RemoveListener(OnGameTimerStoppedOrTimedOut);
        gameTimerEventChannel.Timeout.RemoveListener(OnGameTimerStoppedOrTimedOut);


        scoreTrackerEventChannel.LevelScoringFinished.AddListener(OnLevelScoringFinished);
        levelLifecycleEventChannel.EmitFinishedEvent();
    }

    private void OnLevelScoringFinished(int finishScore)
    {
        scoreTrackerEventChannel.LevelScoringFinished.RemoveListener(OnLevelScoringFinished);

        if (finishScore < LevelDatum.CompletionScore)
        {
            Debug.Log("Lost");
        }
        else
        {
            Debug.Log("Win");
        }
    }
}