using System;
using System.Collections.Generic;
using UnityEngine;

public class Unsubscriber<T> : IDisposable
{
    private List<IObserver<T>> observers;
    private IObserver<T> observer;

    public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
    {
        this.observers = observers;
        this.observer = observer;
    }

    public void Dispose()
    {
        if (observer != null && observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }
}
