using System.Collections;

public class SetGoalSizeWallMovementCommand : IWallMovementCommand
{
    private float goalSize;
    private float duration;

    public SetGoalSizeWallMovementCommand(float newGoalSize, float newDuration)
    {
        goalSize = newGoalSize;
        duration = newDuration;
    }

    public IEnumerator Execute(WallMovementController wallMovementController)
    {
        yield return wallMovementController.SetGoalSize(goalSize,duration);
    }
}

