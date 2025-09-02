using System;

public interface IShapeSpawnDatum
{
	public ShapeType ShapeType { get; }
    public IShapeSpawnStrategy GetShapeSpawnStrategyInstance(Action<ShapeType,int> requestShapeTypeSpawnAction);
}

