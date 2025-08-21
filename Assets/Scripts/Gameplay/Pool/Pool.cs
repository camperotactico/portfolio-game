using UnityEngine;
using UnityEngine.Pool;

public class Pool<T>: IPool<T> where T : MonoBehaviour, IPoolable<T>
{
    private const int DEFAULT_CAPACITY = 8;
    private const int MAX_SIZE = 32;

    private T prefab;
    private Transform instancesParent;
    private IObjectPool<T> objectPool;



    public Pool(T newPrefab, Transform newInstancesParent, int newDefaultCapacity = DEFAULT_CAPACITY, int newMaxSize = MAX_SIZE)
    {
        prefab = newPrefab;
        instancesParent = newInstancesParent;
        objectPool = new ObjectPool<T>(OnCreate, OnRequest, OnRelease, OnDestroy, true, newDefaultCapacity, newMaxSize);
    }

    public T RequestFromPool()
    {
        return objectPool.Get();
    }

    public void ReleaseToPool(T t)
    {
        objectPool.Release(t);
    }


    private T OnCreate()
    {
        T instance = GameObject.Instantiate<T>(prefab);
        instance.transform.SetParent(instancesParent);
        instance.Initialise(ReleaseToPool);
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
        GameObject.Destroy(instance.gameObject);
    }

}

