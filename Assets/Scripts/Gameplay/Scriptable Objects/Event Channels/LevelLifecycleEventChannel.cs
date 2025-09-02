using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LevelLifecycleEventChannel", menuName = "Scriptable Objects/Event Channels/Level Lifecycle Event Channel")]
public class LevelLifecycleEventChannel : ScriptableObject
{

    public UnityEvent<LevelDatum> InitialisationRequested;
    public UnityEvent Started;
    public UnityEvent Finished;


    public void EmitInitialisationRequestedEvent(LevelDatum levelDatum)
    {
        InitialisationRequested?.Invoke(levelDatum);
    }

    public void EmitStartedEvent()
    {
        Started?.Invoke();
    }

    public void EmitFinishedEvent()
    {
        Finished?.Invoke();
    }

}
