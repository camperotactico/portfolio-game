using UnityEngine;
using System;

public abstract class BaseShapeSpawnDatum : ScriptableObject, IShapeSpawnDatum
{
    [Header("Shape Parameters")]
    [SerializeField]
    private ShapeType shapeType;

    [SerializeField]
    private Shape shape;


    public ShapeType ShapeType => shapeType;
    public Shape Shape => shape;

    public abstract IShapeSpawnStrategy GetShapeSpawnStrategyInstance(Action<ShapeType> requestShapeTypeSpawnAction);
}

