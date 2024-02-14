using System;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    public bool IsMining { get; private set; }

    public void SelectForMine()
    {
        IsMining = true;
    }

    public void PickUp()
    {
        Debug.Log("Mined");
    }
}