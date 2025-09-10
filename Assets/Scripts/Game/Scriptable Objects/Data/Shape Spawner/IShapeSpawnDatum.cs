using System;

public interface IShapeSpawnDatum
{
    public ShapeDatum ShapeDatum { get; }
    public IShapeSpawnStrategy GetShapeSpawnStrategyInstance(Action<ShapeDatum, int> requestShapeDatumSpawnAction);
}

