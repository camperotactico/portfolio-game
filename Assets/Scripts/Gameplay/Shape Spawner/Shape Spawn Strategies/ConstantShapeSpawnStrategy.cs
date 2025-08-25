using UnityEngine;
using System;

public class ConstantShapeSpawnStrategy: IShapeSpawnStrategy
{
    private float spawnTime;

    private float remainingTime;

    private ShapeType shapeType;
    private Action<ShapeType> requestShapeSpawnAction;

    public  ConstantShapeSpawnStrategy(ConstantShapeSpawnDatum constantShapeSpawnDatum)
    {
        shapeType = constantShapeSpawnDatum.ShapeType;
        spawnTime = constantShapeSpawnDatum.SpawnTime;
        remainingTime = spawnTime;
    }

    public void SetRequestSpawnAction(Action<ShapeType> newRequestShapeSpawnAction)
    {
        requestShapeSpawnAction = newRequestShapeSpawnAction;
    }

    public void Update(float deltaTime)
    {
        remainingTime -= deltaTime;
        if (remainingTime <= 0f)
        {
            requestShapeSpawnAction?.Invoke(shapeType);
            remainingTime += spawnTime;
        }
    }
}

