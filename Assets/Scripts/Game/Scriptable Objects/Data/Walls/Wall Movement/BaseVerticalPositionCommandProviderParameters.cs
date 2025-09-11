using UnityEngine;

public abstract class BaseVerticalPositionCommandProviderParameters : ScriptableObject, IWallMovementCommandProviderParameters
{
    public abstract ICommandProvider<IWallMovementCommand> GetCommandProviderInstance();
}