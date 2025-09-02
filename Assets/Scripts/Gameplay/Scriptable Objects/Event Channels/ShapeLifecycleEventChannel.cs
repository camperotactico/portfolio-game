using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ShapeLifecycleEventChannel", menuName = "Scriptable Objects/Event Channels/Shape Lifecycle Event Channel")]
public class ShapeLifecycleEventChannel : ScriptableObject
{
    public UnityEvent<Shape, ShapesGoal> EnteredGoal = new UnityEvent<Shape, ShapesGoal>();

    public void EmitEnteredGoal(Shape shape, ShapesGoal shapesGoal)
    {
        EnteredGoal?.Invoke(shape, shapesGoal);
    }
}
