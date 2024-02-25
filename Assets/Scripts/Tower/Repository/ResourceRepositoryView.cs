using TMPro;
using UnityEngine;

public class ResourceRepositoryView : MonoBehaviour
{
    [SerializeField] private ResourceRepository _resourceRepository;
    [SerializeField] private TMP_Text _text;

    private Transform _camera;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void OnEnable()
    {
        _resourceRepository.Changed += OnResourceAdded;
        OnResourceAdded();
    }

    private void OnDisable()
    {
        _resourceRepository.Changed -= OnResourceAdded;
    }

    private void Update()
    {
        if (enabled == false)
            return;

        transform.rotation = _camera.rotation;
    }

    private void OnResourceAdded()
    {
        _text.text = $"Ресурсы: {_resourceRepository.Count}";
    }
}