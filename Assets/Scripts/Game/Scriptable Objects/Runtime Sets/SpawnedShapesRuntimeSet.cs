using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnedShapesRuntimeSet", menuName = "Scriptable Objects/Runtime Sets/Spawned Shapes Runtime Set")]
public class SpawnedShapesRuntimeSet : ScriptableObject
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

    public ICollection<Shape> GetActiveSpawnedShapes()
    {
        ICollection<Shape> allActiveShapes = new List<Shape>();

        foreach (Shape spawnedShape in instanceIDToShape.Values)
        {
            if (!spawnedShape.isActiveAndEnabled)
            {
                continue;
            }
            allActiveShapes.Add(spawnedShape);
        }

        return allActiveShapes;
    }
}
