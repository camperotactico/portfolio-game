using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
{
    private const int DEFAULT_CAPACITY = 8;
    private const int MAX_SIZE = 32;

    private T instancePrefab;
    private Transform instancesParent;
    private IObjectPool<T> objectPool;



    public Pool(T newInstancePrefab, Transform newInstancesParent, int newDefaultCapacity = DEFAULT_CAPACITY, int newMaxSize = MAX_SIZE)
    {
        instancePrefab = newInstancePrefab;
        instancesParent = newInstancesParent;
        objectPool = new ObjectPool<T>(OnCreate, OnRequest, OnRelease, OnDestroy, true, newDefaultCapacity, newMaxSize);
    }

    public T RequestInstance()
    {
        return objectPool.Get();
    }

    public void ReleaseInstance(T t)
    {
        objectPool.Release(t);
    }

    public void Clear()
    {
        objectPool.Clear();
    }


    private T OnCreate()
    {
        T instance = GameObject.Instantiate<T>(instancePrefab);
        instance.transform.SetParent(instancesParent);
        instance.Initialise(ReleaseInstance);
        return instance;
    }

    private void OnRequest(T instance)
    {
        instance.gameObject.SetActive(true);
    }

    private void OnRelease(T instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDestroy(T instance)
    {
        instance.CleanUp();
        GameObject.Destroy(instance.gameObject);
    }

}

