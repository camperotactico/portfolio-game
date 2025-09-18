using UnityEngine;
using UnityEngine.Events;

public class FloatEventChannelListerner : MonoBehaviour
{
    public FloatEventChannel VoidEventChannel;
    public UnityEvent<float> Response;

    void OnEnable()
    {
        VoidEventChannel.AddListener(Response.Invoke);
    }
    void OnDisable()
    {
        VoidEventChannel.AddListener(Response.Invoke);
    }
}
