using System;
using UnityEngine;

public class ResourceRepository : MonoBehaviour
{
    public int Count { get; private set; }

    public event Action Changed;

    public void Add()
    {
        Count++;

        Changed?.Invoke();
    }

    public void Decrease(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException();

        if (Count < value)
            throw new InvalidOperationException();

        Count -= value;

        Changed?.Invoke();
    }
}
