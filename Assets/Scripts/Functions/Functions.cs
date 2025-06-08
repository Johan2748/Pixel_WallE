
public interface ICallable
{
    int Arity { get; }
    object Call(object[] arguments);
}
