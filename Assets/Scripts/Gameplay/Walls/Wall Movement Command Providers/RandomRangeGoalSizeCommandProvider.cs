using UnityEngine;

public class RandomRangeGoalSizeCommandProvider : ICommandProvider<IWallMovementCommand>
{
	private float minRange;
	private float maxRange;
	private float duration;

	public RandomRangeGoalSizeCommandProvider(float newMinRange, float newMaxRange, float newDuration)
	{
		minRange = newMinRange;
        maxRange = newMaxRange;
		duration = newDuration;
    }

    public bool TryGetNext(out IWallMovementCommand nextCommand)
    {
        nextCommand = new SetGoalSizeWallMovementCommand(Random.Range(minRange, maxRange), duration);
        return true;
    }
}