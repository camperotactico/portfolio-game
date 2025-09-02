public class PingPongVerticalPositionCommandProvider : ICommandProvider<IWallMovementCommand>
{

    private float amplitude;
    private float duration;
    private bool isGoingUp;

	public PingPongVerticalPositionCommandProvider(float newAmplitude, float newDuration, bool newIsGoingUp)
	{
        amplitude = newAmplitude;
        duration = newDuration;
        isGoingUp = newIsGoingUp;
	}

    public bool TryGetNext(out IWallMovementCommand nextCommand)
    {
        float verticalPosition = isGoingUp ? amplitude : -amplitude;
        isGoingUp = !isGoingUp;
        nextCommand = new SetVerticalPositionWallMovementCommand(verticalPosition, duration);
        return true;
    }
}


