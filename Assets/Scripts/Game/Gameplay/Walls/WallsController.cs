using UnityEngine;

public class WallsController : MonoBehaviour
{
    public const float WALL_DISTANCE_FROM_ORIGIN = 16f;


    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;

    [Header("Components")]
    [SerializeField]
    private WallMovementController leftWallMovementController;
    [SerializeField]
    private WallMovementController rightWallMovementController;

    private LevelDatum levelDatum;


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

    private void OnLevelInitialisationRequested(LevelDatum newLevelDatum)
    {
        levelDatum = newLevelDatum;
    }

    private void OnLevelStarted()
    {
        leftWallMovementController.StartMovement(levelDatum.LeftWallVerticalPositionParameters.GetCommandProviderInstance(), levelDatum.LeftWallGoalSizeParameters.GetCommandProviderInstance());
        rightWallMovementController.StartMovement(levelDatum.RightWallVerticalPositionParameters.GetCommandProviderInstance(), levelDatum.RightWallGoalSizeParameters.GetCommandProviderInstance());
    }

    private void OnLevelFinished()
    {
        leftWallMovementController.StopMovement();
        rightWallMovementController.StopMovement();
    }

}
