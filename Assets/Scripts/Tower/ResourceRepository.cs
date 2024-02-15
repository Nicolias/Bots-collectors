using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceRepository : MonoBehaviour
{
    private readonly List<ResourceView> _resources = new List<ResourceView>();

    public int Count => _resources.Count;

    public event Action Added;

    public void Add(ResourceView resource)
    {
        if (resource == null)
            throw new ArgumentNullException();

        _resources.Add(resource);

        Added?.Invoke();
    }
}
