using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class ObserverValue<T>
{
    [SerializeField] private T value;
    public Action<T> OnchangeValue = delegate { };
    public T Value
    {
        get => this.value;
        set => Set(value);
    }

    public void Set(T value)
    {
        this.value = value;
        Invoike();
    }

    public void Invoike()
    {
        OnchangeValue?.Invoke(value);
    }
}
