using UnityEngine;

[CreateAssetMenu(fileName = "Platform Pivot Parameters", menuName = "Gameplay/Balancing Platform/Platform Pivot Parameters", order = 0)]
public class PlatformPivotParametes : ScriptableObject
{
    public float DescendingSpeed = 12.0f;
    public float AscendingSpeed = 18.0f;
    public float LowestHeight = -4.0f;
    public float HighestHeight = 0.0f;
}
