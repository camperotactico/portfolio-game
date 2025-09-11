using UnityEngine;

public abstract class BaseGoalSizeCommandProviderParameters : ScriptableObject, IWallMovementCommandProviderParameters
{
    public abstract ICommandProvider<IWallMovementCommand> GetCommandProviderInstance();
}