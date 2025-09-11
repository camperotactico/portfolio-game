using UnityEngine;

public class RandomRangeGoalSizeCommandProvider : ICommandProvider<IWallMovementCommand>
{
    private float minRange;
    private float maxRange;
    private float duration;

    public RandomRangeGoalSizeCommandProvider(RandomRangeGoalSizeCommandProviderParameters parameters)
    {
        minRange = parameters.MinRange;
        maxRange = parameters.MaxRange;
        duration = parameters.Duration;
    }

    public bool TryGetNext(out IWallMovementCommand nextCommand)
    {
        nextCommand = new SetGoalSizeWallMovementCommand(Random.Range(minRange, maxRange), duration);
        return true;
    }
}