using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeDatum", menuName = "Scriptable Objects/Gameplay/Shapes/Shape Datum", order = 0)]
public class ShapeDatum : ScriptableObject
{
	[Range(-1000, 1000)]
	public int Score;
}

