public interface IPool<T>
{
    T RequestFromPool();
    void ReleaseToPool(T t);
}