using System;
using UnityEngine;

public class BalancePlatform : MonoBehaviour
{
    private const float PLATFORM_COLLIDER_SIZE_GAP = 0.05f;

    [Header("Parameters")]
    [SerializeField]
    private BalancePlatformParameters defaultBalancePlatformParameters;
    [SerializeField]
    private PlatformPivotParametes defaultLeftPlatformPivotParameters;
    [SerializeField]
    private PlatformPivotParametes defaultRightPlatformPivotParameters;

    [Header("Components")]
    [SerializeField]
    private BoxCollider2D platformBoxCollider2D;
    [SerializeField]
    private SpriteRenderer platformSpriteRenderer;
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
        leftPlatformPivot.SetInputAction(playerInputActions.Gameplay.LeftButton);
        rightPlatformPivot.SetInputAction(playerInputActions.Gameplay.RightButton);
    }

    public void OnLevelInitialisationRequested(LevelDatum levelDatum)
    {
        SetParameters(levelDatum.BalancePlatformParameters, levelDatum.LeftPlatformPivotParameters, levelDatum.RightPlatformPivotParameters);
    }

    public void OnLevelStarted()
    {
        playerInputActions.Enable();
    }

    public void SetParameters(BalancePlatformParameters newParameters, PlatformPivotParametes newLeftPlatformPivotParameters, PlatformPivotParametes newRightPlatformPivotParameters)
    {
        Vector2 colliderSize = platformBoxCollider2D.size;
        colliderSize.x = newParameters.PlatformLength - PLATFORM_COLLIDER_SIZE_GAP;
        platformBoxCollider2D.size = colliderSize;

        Vector2 spriteSize = platformSpriteRenderer.size;
        spriteSize.x = newParameters.PlatformLength;
        platformSpriteRenderer.size = spriteSize;

        leftPlatformPivot.SetParameters(newLeftPlatformPivotParameters, Vector3.left);
        rightPlatformPivot.SetParameters(newRightPlatformPivotParameters, Vector3.right);
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