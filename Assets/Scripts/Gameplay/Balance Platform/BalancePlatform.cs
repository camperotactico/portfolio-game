using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BalancePlatform : MonoBehaviour
{
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

        leftPlatformPivot.SetInputAction(playerInputActions.Gameplay.LeftButton);
        rightPlatformPivot.SetInputAction(playerInputActions.Gameplay.RightButton);
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