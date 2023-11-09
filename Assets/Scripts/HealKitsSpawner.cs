using UnityEngine;
using UnityEngine.Serialization;

public class HealKitsSpawner : MonoBehaviour
{
    [SerializeField] private HealKit _template;
    [SerializeField] private Transform _spawnedHolder;
    [SerializeField] private Transform _pointsHolder;
    
    private Transform[] _points;

    private void Start()
    {
        CollectSpawnPoints();
        SpawnHealKits();
    }

    private void CollectSpawnPoints()
    {
        _points = new Transform[_pointsHolder.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _pointsHolder.GetChild(i);
        }
    }

    private void SpawnHealKits()
    {
        int minValue = 0;
        int maxValue = 100;
        int spawnChance = 50;
        
        for (int i = 0; i < _points.Length; i++)
        {
            if (Random.Range(minValue, maxValue) > spawnChance)
            {
                Instantiate(_template, _points[i].position, Quaternion.identity, _spawnedHolder);
            }
        }
    }
}
