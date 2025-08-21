using UnityEngine;
using System;

public class Shape : MonoBehaviour, IPoolable<Shape>
{
    private Action<Shape> releaseToPoolAction;

    public void Initialise(Action<Shape> newReleaseToPoolAction)
    {
        releaseToPoolAction = newReleaseToPoolAction;
    }

    public void ReleaseToPool()
    {
        releaseToPoolAction?.Invoke(this);
    }

    private void OnEnable()
    {
        Invoke("Disable", 3.0f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ReleaseToPool();
    }
}

