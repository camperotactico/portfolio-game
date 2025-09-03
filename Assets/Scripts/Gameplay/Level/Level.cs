using System;
using UnityEngine;

public class Level : MonoBehaviour
{

    [Header("Level Parameters")]
    public LevelLoadRequestRuntimeSet levelLoadRequestRuntimeSet;

    [Header("Emitting Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;

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

        levelLifecycleEventChannel.EmitInitialisationRequestedEvent(levelDatum);
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

        if (finishScore < levelDatum.CompletionScore)
        {
            Debug.Log("Lost");
        }
        else
        {
            Debug.Log("Win");
        }
    }
}