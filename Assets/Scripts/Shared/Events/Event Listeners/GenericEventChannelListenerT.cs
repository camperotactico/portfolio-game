using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEventChannelListener<T> : MonoBehaviour
{
    public GenericEventChannel<T> IncomingEvent;
    public UnityEvent<T> Response;

    void OnEnable()
    {
        IncomingEvent.AddListener(Response.Invoke);
    }
    void OnDisable()
    {
        IncomingEvent.AddListener(Response.Invoke);
    }
}
