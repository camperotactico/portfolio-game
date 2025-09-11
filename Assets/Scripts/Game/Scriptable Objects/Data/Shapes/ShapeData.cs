using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ShapeData", menuName = "Scriptable Objects/Data/Shapes/Shapes Data", order = 0)]
public class ShapeData : ScriptableObject
{
    [SerializeField]
    public Shape[] allShapePrefabs;

    private IDictionary<ShapeDatum, Shape> shapeDatumToShapePrefab;

    internal Shape GetShapePrefab(ShapeDatum shapeDatum)
    {
        return LazilyGetShapeDatumToShapePrefab()[shapeDatum];
    }


    private IDictionary<ShapeDatum, Shape> LazilyGetShapeDatumToShapePrefab()
    {
        if (shapeDatumToShapePrefab == null)
        {
            shapeDatumToShapePrefab = new Dictionary<ShapeDatum, Shape>();
            foreach (Shape shapePrefab in allShapePrefabs)
            {
                shapeDatumToShapePrefab[shapePrefab.ShapeDatum] = shapePrefab;
            }
        }
        return shapeDatumToShapePrefab;
    }
}

