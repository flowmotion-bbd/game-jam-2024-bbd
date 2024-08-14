using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable<T>
{
    public void Subscribe(IObserver<T> observer);
    public void NotifySubscribers();
}
