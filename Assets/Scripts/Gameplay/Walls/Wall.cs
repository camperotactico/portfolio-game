using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private WallMovementController movementController;

    private ICommandProvider<IWallMovementCommand> verticalPositionCommandProvider;
    private ICommandProvider<IWallMovementCommand> goalSizeCommandProvider;

    private void Start()
    {
        verticalPositionCommandProvider = new PingPongVerticalPositionCommandProvider(4.5f, 5f, transform.position.x > 0f);
        goalSizeCommandProvider = new RandomRangeGoalSizeCommandProvider(4f, 10f, 3f);
        movementController.StartMovement(verticalPositionCommandProvider, goalSizeCommandProvider);
    }

}
