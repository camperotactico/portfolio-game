using UnityEngine;

[RequireComponent(typeof(WallMovementController))]
public class Wall : MonoBehaviour
{
    public const float WALL_DISTANCE_FROM_ORIGIN = 16f;


    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;

    private WallMovementController movementController;

    private ICommandProvider<IWallMovementCommand> verticalPositionCommandProvider;
    private ICommandProvider<IWallMovementCommand> goalSizeCommandProvider;

    void Awake()
    {
        movementController = GetComponent<WallMovementController>();
    }

    void OnEnable()
    {
        levelLifecycleEventChannel.InitialisationRequested.AddListener(OnLevelInitialisationRequested);
        levelLifecycleEventChannel.Started.AddListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.AddListener(OnLevelFinished);
    }
    void OnDisable()
    {
        levelLifecycleEventChannel.InitialisationRequested.RemoveListener(OnLevelInitialisationRequested);
        levelLifecycleEventChannel.Started.RemoveListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.RemoveListener(OnLevelFinished);
    }

    private void OnLevelInitialisationRequested(LevelDatum levelDatum)
    {
        verticalPositionCommandProvider = new PingPongVerticalPositionCommandProvider(4.5f, 5f, transform.position.x > 0f);
        goalSizeCommandProvider = new RandomRangeGoalSizeCommandProvider(4f, 10f, 3f);
    }

    private void OnLevelStarted()
    {
        movementController.StartMovement(verticalPositionCommandProvider, goalSizeCommandProvider);
    }

    private void OnLevelFinished()
    {
        movementController.StopMovement();
    }

}
