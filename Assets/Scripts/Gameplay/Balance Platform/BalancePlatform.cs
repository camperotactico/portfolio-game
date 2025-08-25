using UnityEngine;

public class BalancePlatform : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private PlatformPivotParametes platformPivotParametes;


    [Header("Components")]
    [SerializeField]
    private Rigidbody2D platform;
    [SerializeField]
    private PlatformPivot leftPlatformPivot;
    [SerializeField]
    private PlatformPivot rightPlatformPivot;



    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    private void Start()
    {

        leftPlatformPivot.SetInputAction(playerInputActions.Gameplay.LeftButton);
        rightPlatformPivot.SetInputAction(playerInputActions.Gameplay.RightButton);

        leftPlatformPivot.SetParameters(platformPivotParametes,Vector3.left);
        rightPlatformPivot.SetParameters(platformPivotParametes,Vector3.right);
    }



    private void FixedUpdate()
    {
        leftPlatformPivot.UpdatePosition(Time.fixedDeltaTime);
        rightPlatformPivot.UpdatePosition(Time.fixedDeltaTime);

        Vector2 leftPivotToRightPivot = rightPlatformPivot.NextPosition - leftPlatformPivot.NextPosition;
        platform.MovePosition(leftPlatformPivot.NextPosition + (0.5f * leftPivotToRightPivot));
        platform.MoveRotation(Vector2.SignedAngle(Vector2.right, leftPivotToRightPivot));
    }
}