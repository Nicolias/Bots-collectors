using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourcePool))]
public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private int _maxResourceExistCount;
    [SerializeField] private List<ResourceSpawnPoint> _resourceSpawnPositions;

    private ResourcePool _pool;

    private void Awake()
    {
        _pool = GetComponent<ResourcePool>();
    }

    public void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            if (_pool.ExistingCount <= _maxResourceExistCount)
                GenerateSpawnPoint().Set(_pool.Get());
        }
    }

    private ResourceSpawnPoint GenerateSpawnPoint()
    {
        List<ResourceSpawnPoint> availablePoints = _resourceSpawnPositions.Where(spawnPoint => spawnPoint.IsSet == false).ToList();

        return availablePoints[Random.Range(0, availablePoints.Count)];
    }
}
