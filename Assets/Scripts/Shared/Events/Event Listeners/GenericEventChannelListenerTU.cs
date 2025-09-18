using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEventChannelListener<T, U> : MonoBehaviour
{
    public GenericEventChannel<T, U> IncomingEvent;
    public UnityEvent<T, U> Response;

    void OnEnable()
    {
        IncomingEvent.AddListener(Response.Invoke);
    }
    void OnDisable()
    {
        IncomingEvent.AddListener(Response.Invoke);
    }
}
