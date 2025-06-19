using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Timer : MonoBehaviour, IObservable<MonoBehaviour>
{
    private List<IObserver<MonoBehaviour>> observers = new List<IObserver<MonoBehaviour>>();

    private bool isRunning = false;
    private float currentTime = 0;
    private float totalTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (isRunning) { currentTime += Time.deltaTime; }

        if (currentTime >= totalTime)
        {
            currentTime = 0;
            isRunning = false;
            NotifyObservers();
        }
    }

    private void NotifyObservers()
    { 
        foreach(var observer in observers) { observer.OnNext(this); }
        foreach(var observer in observers) { observer.OnCompleted(); }
    }

    public IDisposable Subscribe(IObserver<MonoBehaviour> observer)
    {
        if (!observers.Contains(observer)) { observers.Add(observer); }

        return new Unsubscriber<MonoBehaviour>(observers, observer);
    }

    public void StartTimer() { isRunning = true; }

    public void StopTimer() { isRunning = false; }

    public void ResetTimer() { currentTime = 0; }

    public void SetTimer(float time) { totalTime = time; }
}
