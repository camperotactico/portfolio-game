using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ConstantShapeSpawnDatum", menuName = "Scriptable Objects/Data/Shape Spawner/Shape Spawner Strategies/Constant ShapeSpawn Datum", order = 0)]
public class ConstantShapeSpawnDatum : BaseShapeSpawnDatum
{
    [Header("Spawn Strategy Parameters")]
    [Range(0.05f, 10.0f)]
    public float SpawnTime = 0.1f;
    [Range(1, 10)]
    public int SpawnCount = 1;

    public override IShapeSpawnStrategy GetShapeSpawnStrategyInstance(Action<ShapeDatum, int> requestShapeDatumSpawnAction)
    {
        IShapeSpawnStrategy shapeSpawnStrategy = new ConstantShapeSpawnStrategy(this);
        shapeSpawnStrategy.SetRequestSpawnAction(requestShapeDatumSpawnAction);
        return shapeSpawnStrategy;
    }
}

