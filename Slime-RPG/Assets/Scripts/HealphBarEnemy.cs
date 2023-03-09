using UnityEngine;
using UnityEngine.UI;

public class HealphBarEnemy : MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private Image _healthBarEnemyFilling;

    private void Awake()
    {
        _enemy.EnemyHealthChanged += OnHealthEnemyChanged;
    }

    private void OnDestroy()
    {
        _enemy.EnemyHealthChanged -= OnHealthEnemyChanged;
    }

    private void OnHealthEnemyChanged(float valueInPercentage)
    {
        _healthBarEnemyFilling.fillAmount = valueInPercentage;
    }
}
