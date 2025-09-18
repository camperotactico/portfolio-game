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

    private VoidEventChannel GameTimerBegan;
    private VoidEventChannel GameTimerStopped;
    private VoidEventChannel GameTimerTimedOut;
    private FloatEventChannel GameTimerRemainingTimeChanged;
    private FloatEventChannel GameTimerDurationIncreased;
    private FloatEventChannel GameTimerDurationDecreased;

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

        GameTimerBegan = ScriptableObject.CreateInstance<VoidEventChannel>();
        GameTimerStopped = ScriptableObject.CreateInstance<VoidEventChannel>();
        GameTimerTimedOut = ScriptableObject.CreateInstance<VoidEventChannel>();
        GameTimerRemainingTimeChanged = ScriptableObject.CreateInstance<FloatEventChannel>();
        GameTimerDurationIncreased = ScriptableObject.CreateInstance<FloatEventChannel>();
        GameTimerDurationDecreased = ScriptableObject.CreateInstance<FloatEventChannel>();

        gameTimer.Began = GameTimerBegan;
        gameTimer.Stopped = GameTimerStopped;
        gameTimer.TimedOut = GameTimerTimedOut;
        gameTimer.RemainingTimeChanged = GameTimerRemainingTimeChanged;
        gameTimer.DurationIncreased = GameTimerDurationIncreased;
        gameTimer.DurationDecreased = GameTimerDurationDecreased;

    }

    [TearDown]
    public void TearDown()
    {
        if (gameTimer != null)
        {
            gameTimer = null;
        }

        ScriptableObject.Destroy(GameTimerBegan);
        ScriptableObject.Destroy(GameTimerStopped);
        ScriptableObject.Destroy(GameTimerTimedOut);
        ScriptableObject.Destroy(GameTimerRemainingTimeChanged);
        ScriptableObject.Destroy(GameTimerDurationIncreased);
        ScriptableObject.Destroy(GameTimerDurationDecreased);
    }


    [UnityTest]
    public IEnumerator BeginTimerTest()
    {

        bool gameTimerBeganEventCalled = false;
        GameTimerBegan.AddListener(() => gameTimerBeganEventCalled = true);
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
        GameTimerStopped.AddListener(() => gameTimerStoppedEventCalled = true);

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
        GameTimerTimedOut.AddListener(() => gameTimerTimeoutEventCalled = true);

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
        GameTimerStopped.AddListener(() => gameTimerStoppedEventCalled = true);

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
        GameTimerStopped.AddListener(() => gameTimerStoppedEventCalled = true);

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
        GameTimerBegan.AddListener(() => gameTimerBeganEventCalled = true);
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
        GameTimerTimedOut.AddListener(() => hasTimerFinished = true);
        GameTimerRemainingTimeChanged.AddListener((float newRemainingTime) =>
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

        Assert.AreEqual(remainingTime, 0f, EQUALS_DELTA, "The remaining time when `GameTimer.TimedOut()` was emitted is not zero.");

    }

    [UnityTest]
    public IEnumerator IncreaseStartedTimerDurationTest()
    {
        float remainingTime = 0.0f;
        float increasedAmount = 0.0f;

        GameTimerDurationIncreased.AddListener((float newIncreasedDuration) => increasedAmount = newIncreasedDuration);
        GameTimerRemainingTimeChanged.AddListener((float newRemainingTime) => remainingTime = newRemainingTime);
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

        GameTimerDurationDecreased.AddListener((float newDecreasedDuration) => decreasedAmount = newDecreasedDuration);
        GameTimerRemainingTimeChanged.AddListener((float newRemainingTime) => remainingTime = newRemainingTime);
        gameTimer.Begin(START_DURATION);
        gameTimer.DecreaseDuration(EXTRA_DURATION);
        Assert.AreEqual(EXTRA_DURATION, decreasedAmount, EQUALS_DELTA, "`GameTimer.DurationDecreased()` event did not report the correct decreased duration.");
        Assert.AreEqual(START_DURATION - EXTRA_DURATION, remainingTime, EQUALS_DELTA, "`GameTimer.RemainingTimeChanged()` event did not report the correct remaining time.");
        yield return new WaitForEndOfFrame();

        bool hasTimerFinished = false;
        GameTimerTimedOut.AddListener(() => hasTimerFinished = true);
        gameTimer.DecreaseDuration(START_DURATION);

        yield return new WaitForEndOfFrame();

        Assert.IsTrue(hasTimerFinished, "`GameTimer.TimedOut()` was not emitted after decreasing the entirety of the remaining time");
        Assert.AreEqual(0f, remainingTime, EQUALS_DELTA, "The remaining time when `GameTimer.TimedOut()` was emitted is not zero.");


    }


    [UnityTest]
    public IEnumerator IncreaseAndDecreaseStoppedTimerDurationTest()
    {
        bool gameTimerDurationIncreasedEventCalled = false;
        bool gameTimerDurationDecreasedEventCalled = false;
        GameTimerDurationIncreased.AddListener((float newIncreasedDuration) => gameTimerDurationIncreasedEventCalled = true);
        GameTimerDurationDecreased.AddListener((float newDecreasedDuration) => gameTimerDurationDecreasedEventCalled = true);

        gameTimer.Begin(START_DURATION);
        gameTimer.Stop();
        gameTimer.IncreaseDuration(EXTRA_DURATION);
        gameTimer.DecreaseDuration(EXTRA_DURATION);
        yield return new WaitForEndOfFrame();
        Assert.IsFalse(gameTimerDurationIncreasedEventCalled, "`GameTimer.DurationIncreased()` was emitted after calling `gameTimer.IncreaseDuration()`on a stopped timer");
        Assert.IsFalse(gameTimerDurationDecreasedEventCalled, "`GameTimer.DurationDecreased()` was emitted after calling `gameTimer.DecreaseDuration()`on a stopped timer");

    }
}
