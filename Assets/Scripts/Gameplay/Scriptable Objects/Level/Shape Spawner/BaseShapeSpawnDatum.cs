using UnityEngine;
using System;

public abstract class BaseShapeSpawnDatum : ScriptableObject, IShapeSpawnDatum
{
    [Header("Shape Parameters")]
    [SerializeField]
    private ShapeType shapeType;

    public ShapeType ShapeType => shapeType;

    public abstract IShapeSpawnStrategy GetShapeSpawnStrategyInstance(Action<ShapeType,int> requestShapeTypeSpawnAction);
}

