using System;
using UnityEngine;

public class ShapesGoal : MonoBehaviour
{

    [Header("Emitting Event Channels")]
    [SerializeField]
    private ShapeLifecycleEventChannel shapeLifecycleEventChannel;

    [Header("Runtime Sets")]
    [SerializeField]
    private ActiveShapesRuntimeSet activeShapesRuntimeSet;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (activeShapesRuntimeSet.TryGetShape(collider2D.GetInstanceID(), out Shape shape))
        {
            shapeLifecycleEventChannel.EmitEnteredGoal(shape, this);
            shape.ReleaseToPool();
        }
    }
}

