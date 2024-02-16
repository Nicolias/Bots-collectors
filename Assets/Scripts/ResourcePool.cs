using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ResourceView _prefab;

    private Queue<ResourceView> _pool = new Queue<ResourceView>();
    private List<ResourceView> _createdResource = new List<ResourceView>();

    public int ExistingCount => _createdResource.Count - _pool.Count;

    private void OnDisable()
    {
        _createdResource.ForEach(resource => resource.Mined -= Put);
    }

    public ResourceView Get()
    {
        ResourceView resource;

        if (_pool.Count == 0)
        {
            resource = Instantiate(_prefab, _container);
            _createdResource.Add(resource);
        }
        else
        {
            resource = _pool.Dequeue();
        }

        resource.Mined += Put;
        resource.gameObject.SetActive(true);
        resource.Reset();

        return resource;
    }

    private void Put(ResourceView resource)
    {
        resource.Mined -= Put;
        resource.gameObject.SetActive(false);

        _pool.Enqueue(resource);
    }
}