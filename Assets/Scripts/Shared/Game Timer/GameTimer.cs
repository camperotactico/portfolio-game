using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public GameTimerEventChannel GameTimerEventChannel { get => gameTimerEventChannel; set => gameTimerEventChannel = value; }

    [Header("Emitting Event Channels")]
    [SerializeField]
    private GameTimerEventChannel gameTimerEventChannel;

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
            gameTimerEventChannel.EmitRemainingTimeChanged(remainingTime);
        }

        OnTimeout();
    }

    private void OnTimeout()
    {
        StopCoroutine(runningTimerCoroutine);
        runningTimerCoroutine = null;
        remainingTime = 0.0f;
        gameTimerEventChannel.EmitRemainingTimeChanged(remainingTime);
        gameTimerEventChannel.EmitTimeout();
    }

    public void Begin(float initialDuration)
    {
        if (IsRunning())
        {
            return;
        }
        remainingTime = initialDuration;
        runningTimerCoroutine = StartCoroutine(RunTimer());
        gameTimerEventChannel.EmitBegan();
        gameTimerEventChannel.EmitRemainingTimeChanged(remainingTime);
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
        gameTimerEventChannel.EmitStopped();
        gameTimerEventChannel.EmitRemainingTimeChanged(remainingTime);
    }

    public bool IsRunning() { return runningTimerCoroutine != null; }

    public void IncreaseDuration(float durationIncrement)
    {
        if (!IsRunning())
        {
            return;
        }
        remainingTime += durationIncrement;
        gameTimerEventChannel.EmitDurationIncreased(durationIncrement);
        gameTimerEventChannel.EmitRemainingTimeChanged(remainingTime);
    }

    public void DecreaseDuration(float durationDecrement)
    {
        if (!IsRunning())
        {
            return;
        }
        remainingTime -= durationDecrement;
        gameTimerEventChannel.EmitDurationDecreased(durationDecrement);
        if (remainingTime > 0.0)
        {
            gameTimerEventChannel.EmitRemainingTimeChanged(remainingTime);
        }
        else
        {
            OnTimeout();
        }
    }

}
