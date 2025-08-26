using System;
using UnityEngine;

public class ShapesGoal : MonoBehaviour
{
    public static Action<ShapesGoal, Shape> ShapeEntered;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (ActiveShapesCache.TryGetShape(collider2D.GetInstanceID(), out Shape shape))
        {
            ShapeEntered?.Invoke(this, shape);
            shape.ReleaseToPool();
        }
    }
}

