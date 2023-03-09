using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Enemies _enemy;

    private Transform _target;
    private float _attackDistance = 1;
    private float _distanceToPlayer;

    private Animator _animator;

    private int _hp;
    private int _damage;
    private bool _alreadyAttacked;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        _hp = _enemy.HP;
        _damage = _enemy.Damage;

        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (_target != null)
            _distanceToPlayer = (_target.position - transform.position).magnitude;
        else _distanceToPlayer = 0;

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
        if (!_alreadyAttacked & Player.instance != null)
        {
            Player.instance.TakeDamage(_damage);

            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 5 / _enemy.AttackSpeed);

            _animator.SetBool("GetHit", false);
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
        if (Player.instance != null)
            _hp -= Player.instance.Damage;

        _animator.SetBool("GetHit", true);

        if (_hp < 0) DeadEnemy();
    }

    private void DeadEnemy()
    {
        int _currentMoney =  PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", _currentMoney + 10);
        if (_hp <= 0) Destroy(gameObject);
    }
}
