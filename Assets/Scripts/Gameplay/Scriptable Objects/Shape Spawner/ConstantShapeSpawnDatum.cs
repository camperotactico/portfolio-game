using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Constant Shape Spawn Datum", menuName = "Gameplay/Shape Spawner/Shape Spawner Strategies/Constant Shape Spawn Datum", order = 0)]
public class ConstantShapeSpawnDatum : BaseShapeSpawnDatum
{
    [Header("Spawn Strategy Parameters")]
    [Range(0.05f,10.0f)]
	public float SpawnTime = 0.1f;
    [Range(1,10)]
	public int SpawnCount = 1;

    public override IShapeSpawnStrategy GetShapeSpawnStrategyInstance(Action<ShapeType,int> requestShapeTypeSpawnAction)
    {
        IShapeSpawnStrategy shapeSpawnStrategy = new ConstantShapeSpawnStrategy(this);
        shapeSpawnStrategy.SetRequestSpawnAction(requestShapeTypeSpawnAction);
        return shapeSpawnStrategy;
    }
}

