using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTimerTestScript
{

    private const float EQUALS_DELTA = 0.0001f;
    private const float START_DURATION = 2.5f;
    private const float EXTRA_DURATION = 0.5f;

    private GameTimer gameTimer;
    private GameTimerEventChannel gameTimerEventChannel;

    private GameTimer GetGameTimer()
    {
        GameObject gameObject = new GameObject();
        GameTimer gameTimer = gameObject.AddComponent<GameTimer>();
        return gameTimer;
    }


    [SetUp]
    public void SetUp()
    {
        gameTimer = GetGameTimer();
        gameTimerEventChannel = ScriptableObject.CreateInstance<GameTimerEventChannel>();

        gameTimer.GameTimerEventChannel = gameTimerEventChannel;
    }

    [TearDown]
    public void OneTimeTearDown()
    {
        if (gameTimer != null)
        {
            gameTimer = null;
        }

        if (gameTimerEventChannel != null)
        {
            ScriptableObject.Destroy(gameTimerEventChannel);
            gameTimerEventChannel = null;
        }
    }


    [UnityTest]
    public IEnumerator BeginTimerTest()
    {

        bool gameTimerBeganEventCalled = false;
        gameTimerEventChannel.Began.AddListener(() => gameTimerBeganEventCalled = true);
        gameTimer.Begin(START_DURATION);

        yield return new WaitForEndOfFrame();

        Assert.IsTrue(gameTimerBeganEventCalled, "`GameTimer.Began()` event was not called when `gameTimer.Begin(START_DURATION)` was called");
        Assert.IsTrue(gameTimer.IsRunning(), "GameTimer.IsRunning()` should return `true` when the timer began.");
        yield return null;

    }

    [UnityTest]
    public IEnumerator StopTimerTest()
    {
        bool gameTimerStoppedEventCalled = false;
        gameTimerEventChannel.Stopped.AddListener(() => gameTimerStoppedEventCalled = true);

        gameTimer.Begin(START_DURATION);
        yield return new WaitForEndOfFrame();
        gameTimer.Stop();
        yield return new WaitForEndOfFrame();

        Assert.IsTrue(gameTimerStoppedEventCalled, "`GameTimer.Stopped()` event was not called when `gameTimer.Stop()` was called");
        Assert.IsFalse(gameTimer.IsRunning(), "GameTimer.IsRunning()` should return `false` after the timer has been stopped.");


        yield return null;
    }

    [UnityTest]
    public IEnumerator TimeoutTest()
    {
        bool gameTimerTimeoutEventCalled = false;
        gameTimerEventChannel.Timeout.AddListener(() => gameTimerTimeoutEventCalled = true);

        gameTimer.Begin(START_DURATION);
        yield return new WaitForEndOfFrame();

        Assert.IsTrue(gameTimer.IsRunning(), "GameTimer.IsRunning()` should return `true` while the timer is running.");
        yield return new WaitForSeconds(START_DURATION + 0.1f);

        Assert.IsTrue(gameTimerTimeoutEventCalled, "`GameTimer.Timeout()` event was not called when the timer finished.");
        Assert.IsFalse(gameTimer.IsRunning(), "GameTimer.IsRunning()` should return `false` after the timer is finished.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator StopStoppedTimerTest()
    {
        bool gameTimerStoppedEventCalled = false;
        gameTimerEventChannel.Stopped.AddListener(() => gameTimerStoppedEventCalled = true);

        gameTimer.Begin(START_DURATION);
        yield return new WaitForEndOfFrame();
        gameTimer.Stop();
        yield return new WaitForEndOfFrame();
        Assert.IsTrue(gameTimerStoppedEventCalled, "`GameTimer.Stopped()` event was not called when `gameTimer.Stop()` was called the first time.");
        gameTimerStoppedEventCalled = false;
        gameTimer.Stop();
        yield return new WaitForEndOfFrame();
        Assert.IsFalse(gameTimerStoppedEventCalled, "`GameTimer.Stopped()` event was called when `gameTimer.Stop()` was called when the timer was not running");
        yield return null;
    }


    [UnityTest]
    public IEnumerator DestroyStartedGameTimerTest()
    {
        bool gameTimerStoppedEventCalled = false;
        gameTimerEventChannel.Stopped.AddListener(() => gameTimerStoppedEventCalled = true);

        gameTimer.Begin(START_DURATION);
        yield return new WaitForEndOfFrame();
        GameObject.Destroy(gameTimer.gameObject);
        yield return new WaitForEndOfFrame();
        Assert.IsTrue(gameTimerStoppedEventCalled, "`GameTimer.Stopped()` event was not called when `gameTimer` was running.");

    }


    [UnityTest]
    public IEnumerator StartStartedTimerTest()
    {
        bool gameTimerBeganEventCalled = false;
        gameTimerEventChannel.Began.AddListener(() => gameTimerBeganEventCalled = true);
        gameTimer.Begin(START_DURATION);

        yield return new WaitForEndOfFrame();
        Assert.IsTrue(gameTimerBeganEventCalled, "`GameTimer.Began()` event was not called when `gameTimer.Begin(START_DURATION)` was called the first time.");
        gameTimerBeganEventCalled = false;
        gameTimer.Begin(START_DURATION);
        yield return new WaitForEndOfFrame();
        Assert.IsFalse(gameTimerBeganEventCalled, "`GameTimer.Began()` event was called when `gameTimer.Begin(START_DURATION)` was called after the timer was already running.");
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckRemainingDurationTest()
    {
        float previousRemainingTime = START_DURATION;
        float remainingTime = START_DURATION;
        bool hasTimerFinished = false;
        gameTimerEventChannel.Timeout.AddListener(() => hasTimerFinished = true);
        gameTimerEventChannel.RemainingTimeChanged.AddListener((float newRemainingTime) =>
        {
            previousRemainingTime = remainingTime;
            remainingTime = newRemainingTime;
        });
        gameTimer.Begin(START_DURATION);
        while (!hasTimerFinished)
        {
            yield return new WaitForEndOfFrame();
            if (!hasTimerFinished)
            {
                Assert.Less(remainingTime, previousRemainingTime, string.Format("The remainingTime ({0}) has not lowered since the previous frame ({1})", remainingTime, previousRemainingTime));
            }
        }

        Assert.AreEqual(remainingTime, 0f, EQUALS_DELTA, "The remaining time when `GameTimer.Finished()` was emitted is not zero.");

    }

    [UnityTest]
    public IEnumerator IncreaseStartedTimerDurationTest()
    {
        float remainingTime = 0.0f;
        float increasedAmount = 0.0f;

        gameTimerEventChannel.DurationIncreased.AddListener((float newIncreasedDuration) => increasedAmount = newIncreasedDuration);
        gameTimerEventChannel.RemainingTimeChanged.AddListener((float newRemainingTime) => remainingTime = newRemainingTime);
        gameTimer.Begin(START_DURATION);
        gameTimer.IncreaseDuration(EXTRA_DURATION);
        Assert.AreEqual(EXTRA_DURATION, increasedAmount, EQUALS_DELTA, "`GameTimer.DurationIncreased()` event did not report the correct increased duration.");
        Assert.AreEqual(EXTRA_DURATION + START_DURATION, remainingTime, EQUALS_DELTA, "`GameTimer.RemainingTimeChanged()` event did not report the correct remaining time.");
        yield return null;

    }

    [UnityTest]
    public IEnumerator DecreaseStartedTimerDurationTest()
    {
        float remainingTime = 0.0f;
        float decreasedAmount = 0.0f;

        gameTimerEventChannel.DurationDecreased.AddListener((float newDecreasedDuration) => decreasedAmount = newDecreasedDuration);
        gameTimerEventChannel.RemainingTimeChanged.AddListener((float newRemainingTime) => remainingTime = newRemainingTime);
        gameTimer.Begin(START_DURATION);
        gameTimer.DecreaseDuration(EXTRA_DURATION);
        Assert.AreEqual(EXTRA_DURATION, decreasedAmount, EQUALS_DELTA, "`GameTimer.DurationDecreased()` event did not report the correct decreased duration.");
        Assert.AreEqual(START_DURATION - EXTRA_DURATION, remainingTime, EQUALS_DELTA, "`GameTimer.RemainingTimeChanged()` event did not report the correct remaining time.");
        yield return new WaitForEndOfFrame();

        bool hasTimerFinished = false;
        gameTimerEventChannel.Timeout.AddListener(() => hasTimerFinished = true);
        gameTimer.DecreaseDuration(START_DURATION);

        yield return new WaitForEndOfFrame();

        Assert.IsTrue(hasTimerFinished, "`GameTimer.Finished()` was not emitted after decreasing the entirety of the remaining time");
        Assert.AreEqual(remainingTime, 0f, EQUALS_DELTA, "The remaining time when `GameTimer.Finished()` was emitted is not zero.");


    }


    [UnityTest]
    public IEnumerator IncreaseAndDecreaseStoppedTimerDurationTest()
    {
        bool gameTimerDurationIncreasedEventCalled = false;
        bool gameTimerDurationDecreasedEventCalled = false;
        gameTimerEventChannel.DurationIncreased.AddListener((float newIncreasedDuration) => gameTimerDurationIncreasedEventCalled = true);
        gameTimerEventChannel.DurationDecreased.AddListener((float newDecreasedDuration) => gameTimerDurationDecreasedEventCalled = true);

        gameTimer.Begin(START_DURATION);
        gameTimer.Stop();
        gameTimer.IncreaseDuration(EXTRA_DURATION);
        gameTimer.DecreaseDuration(EXTRA_DURATION);
        yield return new WaitForEndOfFrame();
        Assert.IsFalse(gameTimerDurationIncreasedEventCalled, "`GameTimer.DurationIncreased()` was emitted after calling `gameTimer.IncreaseDuration()`on a stopped timer");
        Assert.IsFalse(gameTimerDurationDecreasedEventCalled, "`GameTimer.DurationDecreased()` was emitted after calling `gameTimer.DecreaseDuration()`on a stopped timer");

    }
}
