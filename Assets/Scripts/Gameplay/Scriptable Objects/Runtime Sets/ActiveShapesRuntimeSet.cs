using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveShapesRuntimeSet", menuName = "Scriptable Objects/Runtime Sets/Active Shapes Runtime Set")]
public class ActiveShapesRuntimeSet : ScriptableObject
{
    private IDictionary<int, Shape> instanceIDToShape = new Dictionary<int, Shape>();

    public void OnShapeSpawned(Shape shape)
    {
        instanceIDToShape[shape.ShapeCollider2D.GetInstanceID()] = shape;
    }

    public void OnShapeDestroyed(Shape shape)
    {
        instanceIDToShape.Remove(shape.ShapeCollider2D.GetInstanceID());
    }

    public bool TryGetShape(int collider2DInstanceID, out Shape activeShape)
    {
        return instanceIDToShape.TryGetValue(collider2DInstanceID, out activeShape);
    }
}
