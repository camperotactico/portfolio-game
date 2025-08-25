using System;

public interface IShapeSpawnDatum
{
	public ShapeType ShapeType { get; }
    public Shape Shape { get; }
    public IShapeSpawnStrategy GetShapeSpawnStrategyInstance(Action<ShapeType> requestShapeTypeSpawnAction);
}

