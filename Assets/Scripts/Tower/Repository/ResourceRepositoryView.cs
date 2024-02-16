using TMPro;
using UnityEngine;

public class ResourceRepositoryView : MonoBehaviour
{
    [SerializeField] private ResourceRepository _resourceRepository;

    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _resourceRepository.Changed += OnResourceAdded;
        OnResourceAdded();
    }

    private void OnDisable()
    {
        _resourceRepository.Changed -= OnResourceAdded;
    }

    private void OnResourceAdded()
    {
        _text.text = $"Ресурсы: {_resourceRepository.Count}";
    }
}