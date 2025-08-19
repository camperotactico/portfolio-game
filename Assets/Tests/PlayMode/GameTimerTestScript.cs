using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTimerTestScript
{

    private const float startDuration = 2.5f;
    private const float waitForUnityEventTime = 0.2f;
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
    public IEnumerator BeginTimerTest([Values(startDuration)] float startDuration, [Values(waitForUnityEventTime)]  float waitForUnityEventTime)
    {

        bool gameTimerBeganEventFired = false;
        float waitForUnityEventTimer = waitForUnityEventTime;
        gameTimer.Began.AddListener(() => gameTimerBeganEventFired = true);
        gameTimer.Begin(startDuration);

        while (!gameTimerBeganEventFired && waitForUnityEventTimer >= 0 )
        {
            yield return new WaitForEndOfFrame();
            waitForUnityEventTimer -= Time.deltaTime;
        }

        Assert.IsTrue(gameTimerBeganEventFired,"`GameTimer.Began()` event was not fired when `gameTimer.Begin(startDuration)` was called");
        Assert.IsTrue(gameTimer.IsRunning(),"GameTimer.IsRunning()` should return `true` when the timer began.");
        yield return null;
        
    }

    [UnityTest]
    public IEnumerator StopTimerTest([Values(startDuration)] float startDuration, [Values(waitForUnityEventTime)] float waitForUnityEventTime)
    {
        bool gameTimerStoppedEventCalled = false;
        float waitForUnityEventTimer = waitForUnityEventTime;
        gameTimer.Stopped.AddListener(() => gameTimerStoppedEventCalled = true);

        gameTimer.Begin(startDuration);
        yield return new WaitForEndOfFrame();
        gameTimer.Stop();

        while (!gameTimerStoppedEventCalled && waitForUnityEventTimer >= 0)
        {
            yield return new WaitForEndOfFrame();
            waitForUnityEventTimer -= Time.deltaTime;
        }

        Assert.IsTrue(gameTimerStoppedEventCalled, "`GameTimer.Stopped()` event was not called when `gameTimer.Stop()` was called");
        Assert.IsTrue(gameTimer.IsRunning(), "GameTimer.IsRunning()` should return `false` after the timer has been stopped.");


        yield return null;
    }

    [UnityTest]
    public IEnumerator TimeoutTest()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator StopStoppedTimerTest()
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator StartStartedTimerTest()
    {
        yield return null;
    }
}
