using System;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    public bool IsMining { get; private set; }

    public event Action<ResourceView> Mined;

    public void Reset()
    {
        IsMining = false;
    }

    public void SelectForMine()
    {
        IsMining = true;
    }

    public void PickUp()
    {
        Mined?.Invoke(this);
        gameObject.SetActive(false);
    }
}