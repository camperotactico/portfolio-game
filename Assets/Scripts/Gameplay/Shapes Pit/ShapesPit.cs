using UnityEngine;

public class ShapesPit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (ActiveShapesCache.TryGetShape(collider2D.GetInstanceID(), out Shape shape))
        {
            shape.ReleaseToPool();
        }
    }

}
