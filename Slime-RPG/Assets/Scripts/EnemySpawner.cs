using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<EnemyController> _enemies;
    [SerializeField] private int _enemiesCount;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (_enemiesCount <= _spawnPoints.Count)
        {
            for (int i = 0; i < Random.Range(1, _enemiesCount); i++)
            {
                Transform _spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count - 1)];
                CreateEnemy(Random.Range(0, _enemies.Count - 1), _spawnPoint);
            }
        }
        else
        {
            for (int i = 0; i < Random.Range(1, _spawnPoints.Count); i++)
            {
                Transform _spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count - 1)];
                CreateEnemy(Random.Range(0, _enemies.Count - 1), _spawnPoint);
            }
        }
    }

    private void CreateEnemy(int _count, Transform _spawnPoint)
    {
        EnemyController _newEnemy = Instantiate(_enemies[_count]);
        _newEnemy.transform.position = _spawnPoint.position;
    }
}
