using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Emitting Event Channels")]
    public VoidEventChannel Began;
    public VoidEventChannel Stopped;
    public VoidEventChannel TimedOut;
    public FloatEventChannel RemainingTimeChanged;
    public FloatEventChannel DurationIncreased;
    public FloatEventChannel DurationDecreased;

    private float remainingTime;
    private Coroutine runningTimerCoroutine;


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
            RemainingTimeChanged.Emit(remainingTime);
        }

        OnTimeout();
    }

    private void OnTimeout()
    {
        StopCoroutine(runningTimerCoroutine);
        runningTimerCoroutine = null;
        remainingTime = 0.0f;
        RemainingTimeChanged.Emit(remainingTime);
        TimedOut.Emit();
    }

    public void Begin(float initialDuration)
    {
        if (IsRunning())
        {
            return;
        }
        remainingTime = initialDuration;
        runningTimerCoroutine = StartCoroutine(RunTimer());
        Began.Emit();
        RemainingTimeChanged.Emit(remainingTime);
    }

    public void Stop()
    {
        if (!IsRunning())
        {
            return;
        }
        StopCoroutine(runningTimerCoroutine);
        runningTimerCoroutine = null;
        remainingTime = 0.0f;
        Stopped.Emit();
        RemainingTimeChanged.Emit(remainingTime);
    }

    public bool IsRunning() { return runningTimerCoroutine != null; }

    public void IncreaseDuration(float durationIncrement)
    {
        if (!IsRunning())
        {
            return;
        }
        remainingTime += durationIncrement;
        DurationIncreased.Emit(durationIncrement);
        RemainingTimeChanged.Emit(remainingTime);
    }

    public void DecreaseDuration(float durationDecrement)
    {
        if (!IsRunning())
        {
            return;
        }
        remainingTime -= durationDecrement;
        DurationDecreased.Emit(durationDecrement);
        if (remainingTime > 0.0)
        {
            RemainingTimeChanged.Emit(remainingTime);
        }
        else
        {
            OnTimeout();
        }
    }

}
