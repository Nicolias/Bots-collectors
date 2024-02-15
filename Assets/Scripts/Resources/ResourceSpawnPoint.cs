using UnityEngine;

public class ResourceSpawnPoint : MonoBehaviour
{
    private Transform _transform;

    public bool IsSet { get; private set; }

    private void Awake()
    {
        _transform = transform;
    }

    public void Set(ResourceView resourceView)
    {
        IsSet = true;
        resourceView.transform.position = _transform.position; ;
        resourceView.Mined += OnResourceMined;        
    }

    private void OnResourceMined(ResourceView resourceView)
    {
        resourceView.Mined -= OnResourceMined;
        IsSet = false;
    }
}