using UnityEngine;

[RequireComponent(typeof(WallMovementController))]
public class Wall : MonoBehaviour
{
    public const float WALL_DISTANCE_FROM_ORIGIN = 16f;

    private WallMovementController movementController;

    private ICommandProvider<IWallMovementCommand> verticalPositionCommandProvider;
    private ICommandProvider<IWallMovementCommand> goalSizeCommandProvider;

    void Awake()
    {
        movementController = GetComponent<WallMovementController>();
    }

    public void OnLevelInitialisationRequested(LevelDatum levelDatum)
    {
        SetParameters();
    }

    public void SetParameters()
    {
        verticalPositionCommandProvider = new PingPongVerticalPositionCommandProvider(4.5f, 5f, transform.position.x > 0f);
        goalSizeCommandProvider = new RandomRangeGoalSizeCommandProvider(4f, 10f, 3f);
    }

    public void OnLevelStarted()
    {
        movementController.StartMovement(verticalPositionCommandProvider, goalSizeCommandProvider);
    }

}
