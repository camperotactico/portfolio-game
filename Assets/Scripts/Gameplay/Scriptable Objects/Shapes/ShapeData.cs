using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Shape Data", menuName = "Gameplay/Shapes/Shapes Data", order = 0)]
public class ShapeData : ScriptableObject
{
	[SerializeField]
	public ShapeDatum[] allShapeData;

    private IDictionary<ShapeType, ShapeDatum> shapeTypeToShapeDatum;

    internal Shape GetShapePrefab(ShapeType shapeType)
    {
       return LazilyGetShapeTypeToShapeDatum()[shapeType].Prefab;
    }


    private IDictionary<ShapeType, ShapeDatum> LazilyGetShapeTypeToShapeDatum()
    {
        if (shapeTypeToShapeDatum == null)
        {
            shapeTypeToShapeDatum = new Dictionary<ShapeType, ShapeDatum>();
            foreach (ShapeDatum shapeDatum in allShapeData)
            {
                shapeTypeToShapeDatum[shapeDatum.Type] = shapeDatum;
            }
        }
        return shapeTypeToShapeDatum;
    }
}

