using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeDatum", menuName = "Scriptable Objects/Data/Shapes/Shape Datum", order = 0)]
public class ShapeDatum : ScriptableObject
{
	public Shape ShapePrefab;
	[Range(-1000, 1000)]
	public int Score;
}

