using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Shape Data", menuName = "Gameplay/Shapes/Shapes Data", order = 0)]
public class ShapeData : ScriptableObject
{
	[SerializeField]
	public Shape[] allShapePrefabs;

    private IDictionary<ShapeType, ShapeDatum> shapeTypeToShapeDatum;
    private IDictionary<ShapeType, Shape> shapeTypeToShapePrefab;

    internal ShapeDatum GetShapeDatum(ShapeType shapeType)
    {
       return LazilyGetShapeTypeToShapeDatum()[shapeType];
    }

    private IDictionary<ShapeType, ShapeDatum> LazilyGetShapeTypeToShapeDatum()
    {
        if (shapeTypeToShapeDatum == null)
        {
            shapeTypeToShapeDatum = new Dictionary<ShapeType, ShapeDatum>();
            foreach (Shape shapePrefab in allShapePrefabs)
            {
                shapeTypeToShapeDatum[shapePrefab.ShapeDatum.ShapeType] = shapePrefab.ShapeDatum;
            }
        }
        return shapeTypeToShapeDatum;
    }

    internal Shape GetShapePrefab(ShapeType shapeType)
    {
        return LazilyGetShapeTypeToShapePrefab()[shapeType];
    }


    private IDictionary<ShapeType, Shape> LazilyGetShapeTypeToShapePrefab()
    {
        if (shapeTypeToShapePrefab == null)
        {
            shapeTypeToShapePrefab = new Dictionary<ShapeType, Shape>();
            foreach (Shape shapePrefab in allShapePrefabs)
            {
                shapeTypeToShapePrefab[shapePrefab.ShapeDatum.ShapeType] = shapePrefab;
            }
        }
        return shapeTypeToShapePrefab;
    }
}

