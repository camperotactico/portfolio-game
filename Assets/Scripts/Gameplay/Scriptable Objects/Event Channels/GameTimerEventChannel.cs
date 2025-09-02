using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameTimerEventChannel", menuName = "Scriptable Objects/Event Channels/Game Timer Event Channel")]
public class GameTimerEventChannel : ScriptableObject
{
    public UnityEvent Began = new UnityEvent();
    public UnityEvent Timeout = new UnityEvent();
    public UnityEvent Stopped = new UnityEvent();
    public UnityEvent<float> RemainingTimeChanged = new UnityEvent<float>();
    public UnityEvent<float> DurationIncreased = new UnityEvent<float>();
    public UnityEvent<float> DurationDecreased = new UnityEvent<float>();

    public void EmitBegan()
    {
        Began?.Invoke();
    }

    public void EmitTimeout()
    {
        Timeout?.Invoke();
    }

    public void EmitStopped()
    {
        Stopped?.Invoke();
    }

    public void EmitRemainingTimeChanged(float newRemainingTime)
    {
        RemainingTimeChanged?.Invoke(newRemainingTime);
    }

    public void EmitDurationIncreased(float increasedAmount)
    {
        DurationIncreased?.Invoke(increasedAmount);
    }

    public void EmitDurationDecreased(float decreasedAmount)
    {
        DurationDecreased?.Invoke(decreasedAmount);
    }
}
