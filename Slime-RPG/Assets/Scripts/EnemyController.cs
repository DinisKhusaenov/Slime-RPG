using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _target;
    private float _attackDistance = 1;
    private float _distanceToPlayer;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        _distanceToPlayer = (_target.position - transform.position).magnitude;

        if (_distanceToPlayer > _attackDistance)
        {
            MovementToPlayer();
        }
    }

    private void MovementToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        transform.LookAt(_target.position);
    }
}
