using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTimerTestScript
{

    private const float startDuration = 2.5f;
    private const float waitForUnityEventTime = 0.2f;

    [UnityTest]
    public IEnumerator BeginTimerTest([Values(startDuration)] float startDuration, [Values(waitForUnityEventTime)]  float waitForUnityEventTime)
    {
        GameObject gameObject = new GameObject();
        GameTimer gameTimer = gameObject.AddComponent<GameTimer>();

        bool gameTimerBeganEventFired = false;
        float waitForUnityEventTimer = waitForUnityEventTime;
        gameTimer.Began.AddListener(() => gameTimerBeganEventFired = true);
        gameTimer.Begin(startDuration);

        while (!gameTimerBeganEventFired && waitForUnityEventTimer >= 0 )
        {
            yield return new WaitForEndOfFrame();
            waitForUnityEventTimer -= Time.deltaTime;
        }

        gameTimer.Began.RemoveAllListeners();
        Assert.IsTrue(gameTimerBeganEventFired,"`GameTimer.Began()` event was not fired when `gameTimer.Begin(startDuration)` was called");
        Assert.IsTrue(gameTimer.IsRunning(),"1GameTimer.IsRunning()` should return `true` when the timer began.");
        yield return null;
        
    }

    [UnityTest]
    public IEnumerator StopTimerTest()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator TimeoutTest()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
