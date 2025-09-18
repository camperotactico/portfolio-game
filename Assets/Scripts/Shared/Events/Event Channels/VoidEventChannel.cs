using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Scriptable Objects/Event Channels/Void Event Channel")]
public class VoidEventChannel : ScriptableObject
{
    private UnityEvent unityEvent = new UnityEvent();

    public void Emit()
    {
        unityEvent?.Invoke();
    }

    public void AddListener(UnityAction call)
    {
        unityEvent.AddListener(call);
    }

    public void RemoveListener(UnityAction call)
    {
        unityEvent.RemoveListener(call);
    }
}
