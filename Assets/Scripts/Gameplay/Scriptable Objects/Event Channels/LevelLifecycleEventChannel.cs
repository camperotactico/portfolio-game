using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LevelLifecycleEventChannel", menuName = "Scriptable Objects/Event Channels/Level Lifecycle Event Channel")]
public class LevelLifecycleEventChannel : ScriptableObject
{

    public UnityEvent<LevelDatum> InitialisationRequested = new UnityEvent<LevelDatum>();
    public UnityEvent Started = new UnityEvent();
    public UnityEvent Finished = new UnityEvent();


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
