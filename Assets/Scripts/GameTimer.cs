using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    public UnityEvent Began;
    public UnityEvent Timeout;
    public UnityEvent Stopped;
    public UnityEvent<float> RemainingTimeChanged;
    public UnityEvent<float> DurationIncreased;
    public UnityEvent<float> DurationDecreased;

    private void Awake()
    {
        Began = new UnityEvent();
        Timeout = new UnityEvent();
        Stopped = new UnityEvent();
        RemainingTimeChanged = new UnityEvent<float>();
        DurationIncreased = new UnityEvent<float>();
        DurationDecreased = new UnityEvent<float>();
    }

    public void Begin(float initialDuration) {  }
    public void Stop() { }
    public bool IsRunning() { return false; }
    public void IncreaseDuration(float durationIncrement) { }
    public void DecreaseDuration(float durationDecrement) { }

}
