using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Shape Data", menuName = "Gameplay/Shapes/Shapes Data", order = 0)]
public class ShapeData : ScriptableObject
{
	[SerializeField]
	public ShapeDatum[] allShapeData;
}

