using System;

public interface IShapeSpawnStrategy
{
    void SetRequestSpawnAction(Action<ShapeDatum, int> requestSpawnAction);
    void Update(float deltaTime);
}

