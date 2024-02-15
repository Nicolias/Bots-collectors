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
        if (_pool.Count == 0)
        {
            ResourceView resource = Instantiate(_prefab, _container);
            resource.Mined += Put;
            resource.gameObject.SetActive(true);
            _createdResource.Add(resource);

            return resource;
        }

        return _pool.Dequeue();
    }

    private void Put(ResourceView resource)
    {
        _pool.Enqueue(resource);
    }
}