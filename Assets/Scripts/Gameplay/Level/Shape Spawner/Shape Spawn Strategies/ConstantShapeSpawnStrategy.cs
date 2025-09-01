using UnityEngine;
using System;

public class ConstantShapeSpawnStrategy: IShapeSpawnStrategy
{
    private float spawnTime;
    private int spawnCount;

    private float remainingTime;

    private ShapeType shapeType;
    private Action<ShapeType,int> requestShapeSpawnAction;

    public  ConstantShapeSpawnStrategy(ConstantShapeSpawnDatum constantShapeSpawnDatum)
    {
        shapeType = constantShapeSpawnDatum.ShapeType;
        spawnTime = constantShapeSpawnDatum.SpawnTime;
        spawnCount = constantShapeSpawnDatum.SpawnCount;
        remainingTime = spawnTime;
    }

    public void SetRequestSpawnAction(Action<ShapeType,int> newRequestShapeSpawnAction)
    {
        requestShapeSpawnAction = newRequestShapeSpawnAction;
    }

    public void Update(float deltaTime)
    {
        remainingTime -= deltaTime;
        if (remainingTime <= 0f)
        {
            requestShapeSpawnAction?.Invoke(shapeType,spawnCount);
            remainingTime += spawnTime;
        }
    }
}

