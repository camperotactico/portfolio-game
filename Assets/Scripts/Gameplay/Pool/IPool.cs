public interface IPool<T>
{
    T RequestInstance();
    void ReleaseInstance(T t);
}