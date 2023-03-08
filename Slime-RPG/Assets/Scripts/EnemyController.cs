using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Enemies _enemy;

    private Transform _target;
    private float _attackDistance = 1;
    private float _distanceToPlayer;

    private int _hp;
    private int _damage;
    private bool _alreadyAttacked;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        _hp = _enemy.HP;
        _damage = _enemy.Damage;
    }
    private void FixedUpdate()
    {
        _distanceToPlayer = (_target.position - transform.position).magnitude;

        if (_distanceToPlayer > _attackDistance)
        {
            MovementToPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (!_alreadyAttacked)
        {
            Player.instance.TakeDamage(_damage);

            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 5 / _enemy.AttackSpeed);
        }
    }
    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    private void MovementToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        transform.LookAt(_target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out Projectile _projectile))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _hp -= Player.instance.Damage;

        if (_hp < 0) DeadEnemy();
    }

    private void DeadEnemy()
    {
        if (_hp <= 0) Destroy(gameObject);
    }
}
