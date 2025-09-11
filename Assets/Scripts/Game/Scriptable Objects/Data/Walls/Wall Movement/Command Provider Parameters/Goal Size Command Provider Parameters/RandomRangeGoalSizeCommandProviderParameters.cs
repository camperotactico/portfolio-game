using UnityEngine;

[CreateAssetMenu(fileName = "RandomRangeGoalSizeCommandProviderParameters", menuName = "Scriptable Objects/Data/Walls/Goal Size Command Provider Parameters/Random Range")]
public class RandomRangeGoalSizeCommandProviderParameters : BaseGoalSizeCommandProviderParameters
{
    [Range(0.1f, 15f)]
    public float MinRange = 10f;
    [Range(0.1f, 15f)]
    public float MaxRange = 15f;
    [Range(0.1f, 10f)]
    public float Duration = 3f;

    public override ICommandProvider<IWallMovementCommand> GetCommandProviderInstance()
    {
        return new RandomRangeGoalSizeCommandProvider(this);
    }
}