using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private float _attackForce = 2;
    [SerializeField] private int _damage = 8;
    [SerializeField] private int _hp = 30;

    private Animator _animator;

    private bool _alreadyAttacked;
    private int _maxHp;

    private GameObject[] _enemies;
    private float _stopDistance = 6;

    public static Player instance;
    public event Action<float> HealthChanged;

    public int Damage => _damage;

    public void EnhanceDamage(int _attack)
    {
        _damage += _attack;
    }

    public void EnhanceHP(int _hpUpgrade)
    {
        _maxHp += _hpUpgrade;
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        _animator = GetComponentInChildren<Animator>();
        _maxHp = _hp;

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
                _animator.SetBool("Walk", false);
            }
        }

        if (_enemies.Length == 0)
        {
            PlayerMovement();
            _hp = _maxHp;
            HealthChanged?.Invoke(_hp);
        }
    }

    private void PlayerMovement()
    {
        transform.localPosition += -transform.right * _speed * Time.deltaTime;

        _animator.SetBool("Walk", true);
        _animator.SetBool("GetHit", false);
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

            _animator.SetBool("GetHit", false);
        }
    }
    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    public void TakeDamage(int _damage)
    {
        _animator.SetBool("GetHit", true);

        ChangeHealth(_damage);
    }

    private void ChangeHealth(int _damage)
    {
        _hp -= _damage;

        if (_hp < 0 & gameObject != null) 
        {
            DeadPlayer(); 
        }
        else
        {
            float _currentHealthAsPercantage = (float)_hp / _maxHp;
            HealthChanged?.Invoke(_currentHealthAsPercantage);
        }
    }

    private void DeadPlayer()
    {
        HealthChanged?.Invoke(0);
        Destroy(gameObject);
    }
}
