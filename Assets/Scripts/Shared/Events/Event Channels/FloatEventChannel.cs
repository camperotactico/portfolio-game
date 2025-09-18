using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatEventChannel", menuName = "Scriptable Objects/Event Channels/Float Event Channel")]
public class FloatEventChannel : ScriptableObject
{
    private UnityEvent<float> unityEvent = new UnityEvent<float>();

    public void Emit(float @arg0)
    {
        unityEvent?.Invoke(@arg0);
    }

    public void AddListener(UnityAction<float> call)
    {
        unityEvent.AddListener(call);
    }

    public void RemoveListener(UnityAction<float> call)
    {
        unityEvent.RemoveListener(call);
    }
}
