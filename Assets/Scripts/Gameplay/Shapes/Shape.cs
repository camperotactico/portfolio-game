using UnityEngine;
using System;

public class Shape : MonoBehaviour, IPoolable<Shape>
{
    [Header("Components")]
    [SerializeField]
    private Rigidbody2D shapeRigidbody2D;
    [SerializeField]
    private ShapesPitChecker shapesPitChecker;



    private Action<Shape> releaseToPoolAction;

    public void Initialise(Action<Shape> newReleaseToPoolAction)
    {
        releaseToPoolAction = newReleaseToPoolAction;
    }


    private void OnEnable()
    {
        shapesPitChecker.Fell += OnFellIntoPit;
    }

    private void OnDisable()
    {
        shapesPitChecker.Fell -= OnFellIntoPit;
    }

    private void OnFellIntoPit()
    {
        ReleaseToPool();
    }

    public void ReleaseToPool()
    {
        shapeRigidbody2D.velocity = Vector2.zero;
        shapeRigidbody2D.angularVelocity = 0f;

        releaseToPoolAction?.Invoke(this);
    }

}

