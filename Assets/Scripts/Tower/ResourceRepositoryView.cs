using TMPro;
using UnityEngine;

public class ResourceRepositoryView : MonoBehaviour
{
    [SerializeField] private ResourceRepository _resourceRepository;

    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _resourceRepository.Added += OnResourceAdded;
        OnResourceAdded();
    }

    private void OnDisable()
    {
        _resourceRepository.Added -= OnResourceAdded;
    }

    private void OnResourceAdded()
    {
        _text.text = $"Ресурсы: {_resourceRepository.Count}";
    }
}