using System;

public interface IShapeSpawnStrategy
{
    void SetRequestSpawnAction(Action<ShapeType> requestSpawnAction);
    void Update(float deltaTime);
}

