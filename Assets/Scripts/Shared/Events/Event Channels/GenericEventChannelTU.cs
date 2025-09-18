using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEventChannel<T, U> : ScriptableObject
{
    private UnityEvent<T, U> unityEvent = new UnityEvent<T, U>();

    public void Emit(T arg0, U arg1)
    {
        unityEvent?.Invoke(arg0, arg1);
    }

    public void AddListener(UnityAction<T, U> call)
    {
        unityEvent.AddListener(call);
    }

    public void RemoveListener(UnityAction<T, U> call)
    {
        unityEvent.RemoveListener(call);
    }
}
