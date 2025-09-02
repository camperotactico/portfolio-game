using UnityEngine;

public class ShapesPit : MonoBehaviour
{
    [Header("Runtime Sets")]
    [SerializeField]
    private SpawnedShapesRuntimeSet activeShapesRuntimeSet;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (activeShapesRuntimeSet.TryGetShape(collider2D.GetInstanceID(), out Shape shape))
        {
            shape.ReleaseToPool();
        }
    }

}
