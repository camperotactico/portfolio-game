using System;
using UnityEngine;

[RequireComponent(typeof(GameTimer))]
public class LevelGameTimer : MonoBehaviour
{
    [SerializeField]
    private GameTimer gameTimer;

    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;


    private float levelStartingTime;

    void OnEnable()
    {
        levelLifecycleEventChannel.InitialisationRequested.AddListener(OnLevelInitializationRequested);
        levelLifecycleEventChannel.Started.AddListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.AddListener(OnLevelFinished);
    }

    void OnDisable()
    {
        levelLifecycleEventChannel.InitialisationRequested.RemoveListener(OnLevelInitializationRequested);
        levelLifecycleEventChannel.Started.RemoveListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.RemoveListener(OnLevelFinished);
    }

    private void OnLevelInitializationRequested(LevelDatum levelDatum)
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
