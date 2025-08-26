public interface IPoolable<T>
{
	void Initialise(System.Action<T> releaseToPoolAction);
    void CleanUp();
    void ReleaseToPool();
}

