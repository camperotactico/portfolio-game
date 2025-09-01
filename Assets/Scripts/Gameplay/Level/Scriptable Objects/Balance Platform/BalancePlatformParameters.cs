using UnityEngine;

[CreateAssetMenu(fileName = "Balance Platform Parameters", menuName = "Gameplay/Balance Platform/Balance Platform Parameters")]
public class BalancePlatformParameters : ScriptableObject
{
    [Range(4f, 18f)]
    public float PlatformLength = 18f;
}
