using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Datum", menuName = "Scriptable Objects/Data/Levels/Level Datum", order = 0)]
public class LevelDatum : ScriptableObject
{
    [Header("Level Parameters")]
    [Range(1, 99999)]
    public int ID = 1;
    [Range(5, 180)]
    public int StartingTime = 30;
    public int CompletionScore = 400;

    [Header("Shape Spawning")]
    public BaseShapeSpawnDatum[] ShapeSpawnData;

    [Header("Balance Platform")]
    public BalancePlatformParameters BalancePlatformParameters;
    public PlatformPivotParametes LeftPlatformPivotParameters;
    public PlatformPivotParametes RightPlatformPivotParameters;

    [Header("Left Wall Movement Parameters")]
    public BaseVerticalPositionCommandProviderParameters LeftWallVerticalPositionParameters;
    public BaseGoalSizeCommandProviderParameters LeftWallGoalSizeParameters;

    [Header("Right Wall Movement Parameters")]
    public BaseVerticalPositionCommandProviderParameters RightWallVerticalPositionParameters;
    public BaseGoalSizeCommandProviderParameters RightWallGoalSizeParameters;
}