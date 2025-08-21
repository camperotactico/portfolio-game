using System.Collections;
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

    private float remainingTime;
    private Coroutine runningTimerCoroutine;

    private void Awake()
    {
        Began = new UnityEvent();
        Timeout = new UnityEvent();
        Stopped = new UnityEvent();
        RemainingTimeChanged = new UnityEvent<float>();
        DurationIncreased = new UnityEvent<float>();
        DurationDecreased = new UnityEvent<float>();
    }

    private void OnDestroy()
    {
        Stop();
    }

    private IEnumerator RunTimer()
    {
        while (remainingTime > 0.0f)
        {
            yield return new WaitForEndOfFrame();
            remainingTime = Mathf.Max(0.0f, remainingTime - Time.deltaTime);
            RemainingTimeChanged?.Invoke(remainingTime);
        }

        OnTimeout();
    }

    private void OnTimeout()
    {
        StopCoroutine(runningTimerCoroutine);
        runningTimerCoroutine = null;
        remainingTime = 0.0f;
        RemainingTimeChanged?.Invoke(remainingTime);
        Timeout?.Invoke();
    }

    public void Begin(float initialDuration)
    {
        if (IsRunning())
        {
            return;
        }
        remainingTime = initialDuration;
        runningTimerCoroutine = StartCoroutine(RunTimer());
        Began?.Invoke();
        RemainingTimeChanged?.Invoke(remainingTime);
    }

    public void Stop() {
        if (!IsRunning())
        {
            return;
        }
        StopCoroutine(runningTimerCoroutine);
        runningTimerCoroutine = null;
        remainingTime = 0.0f;
        Stopped?.Invoke();
        RemainingTimeChanged?.Invoke(remainingTime);
    }

    public bool IsRunning() { return runningTimerCoroutine != null; }

    public void IncreaseDuration(float durationIncrement) {
        if (!IsRunning())
        {
            return;
        }
        remainingTime += durationIncrement;
        DurationIncreased?.Invoke(durationIncrement);
        RemainingTimeChanged?.Invoke(remainingTime);
    }

    public void DecreaseDuration(float durationDecrement) {
        if (!IsRunning())
        {
            return;
        }
        remainingTime -= durationDecrement;
        DurationDecreased?.Invoke(durationDecrement);
        if (remainingTime > 0.0)
        {
            RemainingTimeChanged?.Invoke(remainingTime);
        }
        else
        {
            OnTimeout();
        }
    }

}
