using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    bool _started;
    float _elapsed;
    float _duration;

    public event Action TimerRunOut;

    // Update is called once per frame
    void Update()
    {
        if (_started)
        {
            _elapsed += Time.deltaTime; 
            if (_elapsed >= _duration) 
            { 
                _started = false;
                _elapsed = 0;
                TimerRunOut?.Invoke();
            }
        }
    }

    public void StartTimer(float duration)
    {
        _started = true;
        _duration = duration;
        _elapsed = 0;
    }

    public void StopTimer()
    {
        if (_started)
        {
            _started = false;
        }
    }

    public void AddEventListener(Action handler)
    {
        TimerRunOut += handler;
    }
}
