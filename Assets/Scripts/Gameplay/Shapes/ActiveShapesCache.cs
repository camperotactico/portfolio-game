using System.Collections.Generic;

public static class ActiveShapesCache
{
    private static IDictionary<int, Shape> instanceIDToShape = new Dictionary<int,Shape>();

    public static void OnShapeSpawned(Shape shape)
    {
        instanceIDToShape[shape.ShapeCollider2D.GetInstanceID()] = shape;
    }

    public static void OnShapeDestroyed(Shape shape)
    {
        instanceIDToShape.Remove(shape.ShapeCollider2D.GetInstanceID());
    }

    public static bool TryGetShape(int collider2DInstanceID, out Shape activeShape)
    {
        return instanceIDToShape.TryGetValue(collider2DInstanceID, out activeShape);
    }

}

