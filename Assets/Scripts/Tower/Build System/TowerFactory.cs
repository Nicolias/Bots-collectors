using TowerBuildSystem;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private GestureClick _tirrain;
    [SerializeField] private CoroutineServise _coroutineServise;

    [SerializeField] private Tower _towerTemplate;

    [SerializeField] private Vector3 _firstTowerSpawnPosition;

    private void Awake()
    {
        Create(_firstTowerSpawnPosition);
    }

    public Tower Create(Vector3 buildPosition)
    {
        Tower tower = Instantiate(_towerTemplate, buildPosition, Quaternion.identity);

        tower.Initialize(_tirrain, _coroutineServise, this);
        tower.enabled = true;

        return tower;
    }
}
