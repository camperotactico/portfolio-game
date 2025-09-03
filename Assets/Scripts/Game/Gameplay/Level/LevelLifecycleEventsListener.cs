using System;
using UnityEngine;
using UnityEngine.Events;

// This is a class that allows Designers and Artist to implement level lifecyle event responses straight from the inspector.
public class LevelLifecycleEventsListener : MonoBehaviour
{
    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;

    [Header("Forwarded Events")]
    [SerializeField]
    private UnityEvent<LevelDatum> levelInitialisationRequested;
    [SerializeField]
    private UnityEvent levelStarted;
    [SerializeField]
    private UnityEvent levelFinished;

    void OnEnable()
    {
        levelLifecycleEventChannel.InitialisationRequested.AddListener(OnLevelInitialisationRequested);
        levelLifecycleEventChannel.Started.AddListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.AddListener(OnLevelFinished);
    }

    void OnDisable()
    {
        levelLifecycleEventChannel.InitialisationRequested.RemoveListener(OnLevelInitialisationRequested);
        levelLifecycleEventChannel.Started.RemoveListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.RemoveListener(OnLevelFinished);
    }

    private void OnLevelInitialisationRequested(LevelDatum levelDatum)
    {
        levelInitialisationRequested?.Invoke(levelDatum);
    }

    private void OnLevelFinished()
    {
        levelFinished?.Invoke();
    }

    private void OnLevelStarted()
    {
        levelStarted?.Invoke();
    }
}
