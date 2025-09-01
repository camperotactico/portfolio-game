using System.Collections;

public class SetVerticalPositionWallMovementCommand : IWallMovementCommand
{
    private float verticalPosition;
    private float duration;

    public SetVerticalPositionWallMovementCommand(float newVerticalPosition, float newDuration)
    {
        verticalPosition = newVerticalPosition;
        duration = newDuration;
    }

    public IEnumerator Execute(WallMovementController wallMovementController)
    {
        yield return wallMovementController.SetVerticalPosition(verticalPosition,duration);
    }
}

