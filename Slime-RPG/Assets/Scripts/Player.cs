using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private GameObject[] _enemies;
    private float _stopDistance = 4;
    
    private void Update()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < _enemies.Length; i++)
        {
            float _distanceToEnemy = (_enemies[i].transform.position - transform.position).magnitude;

            if ( _distanceToEnemy > _stopDistance)
            {
                PlayerMovement();
            }
        }
    }

    private void PlayerMovement()
    {
        transform.localPosition += -transform.right * _speed * Time.deltaTime;
    }
}
