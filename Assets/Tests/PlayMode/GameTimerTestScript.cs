using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTimerTestScript
{

    private const float START_DURATION = 2.5f;
    private const float EXTRA_DURATION = 0.5f;
    private GameTimer gameTimer;

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
    }

    [TearDown]
    public void OneTimeTearDown()
    {
        gameTimer.Began.RemoveAllListeners();
        gameTimer = null;
    }


    [UnityTest]
    public IEnumerator BeginTimerTest([Values(START_DURATION)] float startDuration)
    {

        bool gameTimerBeganEventCalled = false;
        gameTimer.Began.AddListener(() => gameTimerBeganEventCalled = true);
        gameTimer.Begin(startDuration);

        yield return new WaitForEndOfFrame();

        Assert.IsTrue(gameTimerBeganEventCalled,"`GameTimer.Began()` event was not called when `gameTimer.Begin(startDuration)` was called");
        Assert.IsTrue(gameTimer.IsRunning(),"GameTimer.IsRunning()` should return `true` when the timer began.");
        yield return null;
        
    }

    [UnityTest]
    public IEnumerator StopTimerTest([Values(START_DURATION)] float startDuration)
    {
        bool gameTimerStoppedEventCalled = false;
        gameTimer.Stopped.AddListener(() => gameTimerStoppedEventCalled = true);

        gameTimer.Begin(startDuration);
        yield return new WaitForEndOfFrame();
        gameTimer.Stop();
        yield return new WaitForEndOfFrame();
         
        Assert.IsTrue(gameTimerStoppedEventCalled, "`GameTimer.Stopped()` event was not called when `gameTimer.Stop()` was called");
        Assert.IsFalse(gameTimer.IsRunning(), "GameTimer.IsRunning()` should return `false` after the timer has been stopped.");


        yield return null;
    }

    [UnityTest]
    public IEnumerator TimeoutTest([Values(START_DURATION)] float startDuration)
    {
        bool gameTimerTimeoutEventCalled = false;
        gameTimer.Timeout.AddListener(() => gameTimerTimeoutEventCalled = true);

        gameTimer.Begin(startDuration);
        yield return new WaitForEndOfFrame();

        Assert.IsTrue(gameTimer.IsRunning(), "GameTimer.IsRunning()` should return `true` while the timer is running.");
        yield return new WaitForSeconds(startDuration);

        Assert.IsTrue(gameTimerTimeoutEventCalled, "`GameTimer.Timeout()` event was not called when the timer finished.");
        Assert.IsFalse(gameTimer.IsRunning(), "GameTimer.IsRunning()` should return `false` after the timer is finished.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator StopStoppedTimerTest([Values(START_DURATION)] float startDuration)
    {
        bool gameTimerStoppedEventCalled = false;
        gameTimer.Stopped.AddListener(() => gameTimerStoppedEventCalled = true);

        gameTimer.Begin(startDuration);
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
    public IEnumerator StartStartedTimerTest([Values(START_DURATION)] float startDuration)
    {
        bool gameTimerBeganEventCalled = false;
        gameTimer.Began.AddListener(() => gameTimerBeganEventCalled = true);
        gameTimer.Begin(startDuration);

        yield return new WaitForEndOfFrame();
        Assert.IsTrue(gameTimerBeganEventCalled, "`GameTimer.Began()` event was not called when `gameTimer.Begin(startDuration)` was called the first time.");
        gameTimerBeganEventCalled = false;
        gameTimer.Begin(startDuration);
        yield return new WaitForEndOfFrame();
        Assert.IsFalse(gameTimerBeganEventCalled, "`GameTimer.Began()` event was called when `gameTimer.Begin(startDuration)` was called after the timer was already running.");
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckRemainingDurationTest([Values(START_DURATION)] float startDuration)
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator IncreaseStartedTimerDurationTest([Values(START_DURATION)] float startDuration, [Values(EXTRA_DURATION)] float durationIncrease)
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator DecreaseStartedTimerDurationTest([Values(START_DURATION)] float startDuration, [Values(EXTRA_DURATION)] float durationDecrease)
    {
        yield return null;
    }


    [UnityTest]
    public IEnumerator IncreaseAndDecreaseStoppedTimerDurationTest([Values(START_DURATION)] float startDuration, [Values(EXTRA_DURATION)] float extraDuration)
    {
        yield return null;
    }
}
