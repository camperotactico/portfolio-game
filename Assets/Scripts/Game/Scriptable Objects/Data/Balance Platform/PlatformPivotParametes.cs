using UnityEngine;

[CreateAssetMenu(fileName = "PlatformPivotParameters", menuName = "Scriptable Objects/Data/Balance Platform/Platform Pivot Parameters", order = 0)]
public class PlatformPivotParametes : ScriptableObject
{
    public float DistanceFromCenter = 4.5f;
    public float DescendingSpeed = 12.0f;
    public float AscendingSpeed = 18.0f;
    public float Range = 4.0f;

}
