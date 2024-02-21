using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IObserverArray<T>
{
    public event Action<T[]> OnArrayChange;
    public T Get(int index);
    public bool TryAdd(T value);
    public bool TryRemove(T value);
    public void Swap(int index1, int index2);
    public void Clear();
}
[Serializable]
public class ObserverArray<T> : IObserverArray<T>
{
    [SerializeField] private T[] items;
    public event Action<T[]> OnArrayChange = delegate { };

    public ObserverArray(int size)
    {
        items = new T[size];
    }

    public T this[int index] => items[index];

    public T Get(int index)
    {
        return items[index];
    }

    public void Swap(int index1, int index2)
    {
        (items[index1], items[index2]) = (items[index2], items[index1]);
        Invoke();
    }

    public bool TryAdd(T value)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null) continue;
            items[i] = value;
            Invoke();
            return true;
        }
        return false;
    }

    public bool TryRemove(T value)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) continue;
            if (!EqualityComparer<T>.Default.Equals(items[i], value)) continue;
            this.items[i] = default;
            Invoke();
            return true;
        }
        return false;
    }

    public void Invoke() => OnArrayChange?.Invoke(this.items);

    public void Clear()
    {
        this.items = new T[this.items.Length];
    }
}

