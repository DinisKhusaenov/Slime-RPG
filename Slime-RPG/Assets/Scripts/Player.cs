using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private float _attackForce = 2;
    [SerializeField] private int _damage = 8;
    [SerializeField] private int _hp = 30;


    private bool _alreadyAttacked;

    private GameObject[] _enemies;
    private float _stopDistance = 6;


    public static Player instance;

    public int Damage => _damage;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

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
            else
            {
                AttackEnemy(_enemies[i]);
            }
        }

        if (_enemies.Length == 0)
            PlayerMovement();
    }

    private void PlayerMovement()
    {
        transform.localPosition += -transform.right * _speed * Time.deltaTime;
    }

    private void AttackEnemy(GameObject _enemy)
    {
        Vector3 _attackDirection = (_enemy.transform.position - transform.position);

        if (!_alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(_projectile, transform.position + new Vector3 (-0.5f, 0.5f, 0), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(_attackDirection * _attackForce, ForceMode.Impulse);
            rb.AddForce(transform.up * 2.5f, ForceMode.Impulse);

            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    public void TakeDamage(int _damage)
    {
        _hp -= _damage;

        if (_hp < 0) DeadPlayer();
    }

    private void DeadPlayer()
    {
        Destroy(gameObject);
    }
}
