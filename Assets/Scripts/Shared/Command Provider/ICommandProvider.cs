public interface ICommandProvider<T>
{
    public bool TryGetNext(out T nextCommand);
}

