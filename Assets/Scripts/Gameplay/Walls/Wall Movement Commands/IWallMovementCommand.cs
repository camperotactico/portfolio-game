using System.Collections;

public interface IWallMovementCommand
{
    public IEnumerator Execute(WallMovementController wallMovementController);
}

