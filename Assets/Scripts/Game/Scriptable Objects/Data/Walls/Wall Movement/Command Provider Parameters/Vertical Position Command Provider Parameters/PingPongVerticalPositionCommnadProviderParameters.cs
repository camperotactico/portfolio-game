using UnityEngine;

[CreateAssetMenu(fileName = "PingPongVerticalPositionCommnadProviderParameters", menuName = "Scriptable Objects/Data/Walls/Vertical Position Command Provider Parameters/Ping Pong")]
public class PingPongVerticalPositionCommnadProviderParameters : BaseVerticalPositionCommandProviderParameters
{
    [Range(0.1f, 10f)]
    public float Amplitude = 4.5f;
    [Range(0.1f, 10f)]
    public float Duration = 5f;
    public bool StartsGoingUp;

    public override ICommandProvider<IWallMovementCommand> GetCommandProviderInstance()
    {
        return new PingPongVerticalPositionCommandProvider(this);
    }
}