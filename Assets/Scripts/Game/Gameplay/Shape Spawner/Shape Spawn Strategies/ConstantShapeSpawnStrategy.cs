using System;

public class ConstantShapeSpawnStrategy : IShapeSpawnStrategy
{
    private float spawnTime;
    private int spawnCount;

    private float remainingTime;

    private ShapeDatum shapeDatum;
    private Action<ShapeDatum, int> requestShapeSpawnAction;

    public ConstantShapeSpawnStrategy(ConstantShapeSpawnDatum constantShapeSpawnDatum)
    {
        shapeDatum = constantShapeSpawnDatum.ShapeDatum;
        spawnTime = constantShapeSpawnDatum.SpawnTime;
        spawnCount = constantShapeSpawnDatum.SpawnCount;
        remainingTime = spawnTime;
    }

    public void SetRequestSpawnAction(Action<ShapeDatum, int> newRequestShapeSpawnAction)
    {
        requestShapeSpawnAction = newRequestShapeSpawnAction;
    }

    public void Update(float deltaTime)
    {
        remainingTime -= deltaTime;
        if (remainingTime <= 0f)
        {
            requestShapeSpawnAction?.Invoke(shapeDatum, spawnCount);
            remainingTime += spawnTime;
        }
    }
}

