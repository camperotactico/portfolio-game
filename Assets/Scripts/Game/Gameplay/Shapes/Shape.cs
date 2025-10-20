using UnityEngine;
using System;

public class Shape : MonoBehaviour, IPoolable<Shape>
{
    public Collider2D ShapeCollider2D { get => shapeCollider2D; }
    public ShapeDatum ShapeDatum { get => shapeDatum; }


    [Header("Parameters")]
    [SerializeField]
    private ShapeDatum shapeDatum;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D shapeRigidbody2D;
    [SerializeField]
    private Collider2D shapeCollider2D;

    private Action<Shape> _releaseToPoolAction;


    public void Initialise(Action<Shape> newReleaseToPoolAction)
    {
        _releaseToPoolAction = newReleaseToPoolAction;
    }

    public void ReleaseToPool()
    {
        shapeRigidbody2D.linearVelocity = Vector2.zero;
        shapeRigidbody2D.angularVelocity = 0f;

        _releaseToPoolAction?.Invoke(this);
    }

    public void CleanUp()
    {
    }
}

