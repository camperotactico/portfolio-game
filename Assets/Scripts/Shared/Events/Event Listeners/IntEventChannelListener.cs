using UnityEngine;
using UnityEngine.Events;

public class IntEventChannelListerner : MonoBehaviour
{
    public IntEventChannel VoidEventChannel;
    public UnityEvent<int> Response;

    void OnEnable()
    {
        VoidEventChannel.AddListener(Response.Invoke);
    }
    void OnDisable()
    {
        VoidEventChannel.AddListener(Response.Invoke);
    }
}
