using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEventChannel<T> : ScriptableObject
{
    private UnityEvent<T> unityEvent = new UnityEvent<T>();

    public void Emit(T arg0)
    {
        unityEvent?.Invoke(arg0);
    }

    public void AddListener(UnityAction<T> call)
    {
        unityEvent.AddListener(call);
    }

    public void RemoveListener(UnityAction<T> call)
    {
        unityEvent.RemoveListener(call);
    }
}
