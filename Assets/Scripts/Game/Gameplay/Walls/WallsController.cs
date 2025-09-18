using UnityEngine;

public class WallsController : MonoBehaviour
{
    public const float WALL_DISTANCE_FROM_ORIGIN = 16f;


    [Header("Receiving Event Channels")]
    public LevelDatumEventChannel LevelInitialisationRequested;
    public VoidEventChannel LevelStarted;
    public VoidEventChannel LevelFinished;


    [Header("Components")]
    [SerializeField]
    private WallMovementController leftWallMovementController;
    [SerializeField]
    private WallMovementController rightWallMovementController;

    private LevelDatum levelDatum;


    void OnEnable()
    {
        LevelInitialisationRequested.AddListener(OnLevelInitialisationRequested);
        LevelStarted.AddListener(OnLevelStarted);
        LevelFinished.AddListener(OnLevelFinished);
    }
    void OnDisable()
    {
        LevelInitialisationRequested.RemoveListener(OnLevelInitialisationRequested);
        LevelStarted.RemoveListener(OnLevelStarted);
        LevelFinished.RemoveListener(OnLevelFinished);
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
