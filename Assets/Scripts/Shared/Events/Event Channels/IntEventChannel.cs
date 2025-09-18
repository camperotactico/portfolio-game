using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntEventChannel", menuName = "Scriptable Objects/Event Channels/Int Event Channel")]
public class IntEventChannel : ScriptableObject
{
    private UnityEvent<int> unityEvent = new UnityEvent<int>();

    public void Emit(int @arg0)
    {
        unityEvent?.Invoke(@arg0);
    }

    public void AddListener(UnityAction<int> call)
    {
        unityEvent.AddListener(call);
    }

    public void RemoveListener(UnityAction<int> call)
    {
        unityEvent.RemoveListener(call);
    }
}
