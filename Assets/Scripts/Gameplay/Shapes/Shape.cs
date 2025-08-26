using UnityEngine;
using System;

public class Shape : MonoBehaviour, IPoolable<Shape>
{
    public Collider2D ShapeCollider2D { get => shapeCollider2D; }
    public ShapeDatum ShapeDatum { get => shapeDatum; }


    [Header("Paramters")]
    [SerializeField]
    private ShapeDatum shapeDatum;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D shapeRigidbody2D;
    [SerializeField]
    private Collider2D shapeCollider2D;


    private Action<Shape> releaseToPoolAction;


    public void Initialise(Action<Shape> newReleaseToPoolAction)
    {
        releaseToPoolAction = newReleaseToPoolAction;
        ActiveShapesCache.OnShapeSpawned(this);
    }

    public void ReleaseToPool()
    {
        shapeRigidbody2D.velocity = Vector2.zero;
        shapeRigidbody2D.angularVelocity = 0f;

        releaseToPoolAction?.Invoke(this);
    }

    public void CleanUp()
    {
        ActiveShapesCache.OnShapeDestroyed(this);
    }
}

