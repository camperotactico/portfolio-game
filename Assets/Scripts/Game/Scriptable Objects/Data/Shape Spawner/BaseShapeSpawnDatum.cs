using UnityEngine;
using System;

public abstract class BaseShapeSpawnDatum : ScriptableObject, IShapeSpawnDatum
{
    [Header("Shape Parameters")]
    [SerializeField]
    private ShapeDatum shapeDatum;

    public ShapeDatum ShapeDatum => shapeDatum;

    public abstract IShapeSpawnStrategy GetShapeSpawnStrategyInstance(Action<ShapeDatum, int> requestShapeDatumSpawnAction);
}

