using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    public UnityEvent Began;
    public UnityEvent Stopped;
    public UnityEvent<float> RemainingTimeChanged;
    public UnityEvent DurationIncreased;
    public UnityEvent DurationDecreased;

    private void Awake()
    {
        Began = new UnityEvent();
        Stopped = new UnityEvent();
        RemainingTimeChanged = new UnityEvent<float>();
        DurationIncreased = new UnityEvent();
        DurationDecreased = new UnityEvent();
    }

    public void Begin(float initialDuration) {  }
    public void Stop() { throw new System.Exception("Not tested."); }
    public bool IsRunning() { return false; }
    public void IncreaseDuration(float durationIncrement) { throw new System.Exception("Not tested."); }
    public void DecreaseDuration(float durationDecrement) { throw new System.Exception("Not tested."); }
}
