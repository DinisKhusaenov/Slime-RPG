using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Road[] _roads;
    [SerializeField] private Road _startRoad;
    [SerializeField] private Road _secondRoad;

    private List<Road> _spawnedRoad = new List<Road>();

    private void Start()
    {
        _spawnedRoad.Add(_startRoad); 
        _spawnedRoad.Add(_secondRoad); 
    }

    private void Update()
    {
        if (_player.position.x < _spawnedRoad[_spawnedRoad.Count - 2].End.position.x)
        {
            SpawnRoad();
        }
    }

    private void SpawnRoad()
    {
        Road _newRoad = Instantiate(_roads[Random.Range(0, _roads.Length)]);
        _newRoad.transform.position = _spawnedRoad[_spawnedRoad.Count - 1].End.position - _newRoad.Begin.localPosition;
        _spawnedRoad.Add(_newRoad);

        DestroyRoad();
    }

    private void DestroyRoad()
    {
        if (_spawnedRoad.Count > 3)
        {
            Destroy(_spawnedRoad[0].gameObject);
            _spawnedRoad.RemoveAt(0);
        }
    }

}
