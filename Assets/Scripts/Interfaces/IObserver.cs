public interface IObserver<T>
{
    public void StateChanged(T state);
}
