using System.Collections;
using UnityEngine;

public class WaitWallMovementCommand : IWallMovementCommand
{
    private float waitTime;

    public WaitWallMovementCommand(float newWaitTime)
    {
        waitTime = newWaitTime;
    }

    public IEnumerator Execute(WallMovementController wallMovementController)
    {
        yield return new WaitForSeconds(waitTime);
    }
}

