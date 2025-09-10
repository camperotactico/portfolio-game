using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LevelDataAvailabilityEventChannel", menuName = "Scriptable Objects/Event Channels/Level Data Availability Event Channel")]
public class LevelDataAvailabilityEventChannel : ScriptableObject
{
    public UnityEvent LevelDataRequested = new UnityEvent();
    public UnityEvent LevelDataReady = new UnityEvent();

    public void EmitLevelDataRequested()
    {
        LevelDataRequested?.Invoke();
    }

    public void EmitLevelDataReady()
    {
        LevelDataReady?.Invoke();
    }
}