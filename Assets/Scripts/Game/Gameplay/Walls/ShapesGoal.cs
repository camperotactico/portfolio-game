using UnityEngine;

public class ShapesGoal : MonoBehaviour
{
    [Header("Emitting Event Channels")]
    [SerializeField]
    private ShapeShapesGoalEventChannel ShapeEnteredShapesGoal;

    [Header("Runtime Sets")]
    [SerializeField]
    private SpawnedShapesRuntimeSet activeShapesRuntimeSet;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (activeShapesRuntimeSet.TryGetShape(collider2D.GetInstanceID(), out Shape shape))
        {
            ShapeEnteredShapesGoal.Emit(shape, this);
            shape.ReleaseToPool();
        }
    }
}

