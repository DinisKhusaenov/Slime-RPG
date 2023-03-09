using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private Player _player;

    private Camera _camera;

    private void Awake()
    {
        _player.HealthChanged += OnHealthChanged;
        _camera = Camera.main;
    }

    private void OnDestroy()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float valueInPercentage)
    {
        _healthBarFilling.fillAmount = valueInPercentage;
    }
}
