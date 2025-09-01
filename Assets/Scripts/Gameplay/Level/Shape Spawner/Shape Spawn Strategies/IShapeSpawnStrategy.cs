using System;

public interface IShapeSpawnStrategy
{
    void SetRequestSpawnAction(Action<ShapeType,int> requestSpawnAction);
    void Update(float deltaTime);
}

