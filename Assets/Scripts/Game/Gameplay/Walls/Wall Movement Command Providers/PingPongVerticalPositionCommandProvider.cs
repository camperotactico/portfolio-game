public class PingPongVerticalPositionCommandProvider : ICommandProvider<IWallMovementCommand>
{
    private float amplitude;
    private float duration;
    private bool isGoingUp;

    public PingPongVerticalPositionCommandProvider(PingPongVerticalPositionCommnadProviderParameters parameters)
    {
        amplitude = parameters.Amplitude;
        duration = parameters.Duration;
        isGoingUp = parameters.StartsGoingUp;
    }

    public bool TryGetNext(out IWallMovementCommand nextCommand)
    {
        float verticalPosition = isGoingUp ? amplitude : -amplitude;
        isGoingUp = !isGoingUp;
        nextCommand = new SetVerticalPositionWallMovementCommand(verticalPosition, duration);
        return true;
    }
}


