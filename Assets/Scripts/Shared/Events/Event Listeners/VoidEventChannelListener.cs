using UnityEngine;
using UnityEngine.Events;

public class VoidEventChannelListerner : MonoBehaviour
{
    public VoidEventChannel VoidEventChannel;
    public UnityEvent Response;

    void OnEnable()
    {
        VoidEventChannel.AddListener(Response.Invoke);
    }
    void OnDisable()
    {
        VoidEventChannel.AddListener(Response.Invoke);
    }
}
