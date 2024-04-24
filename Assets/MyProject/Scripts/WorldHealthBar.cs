using UnityEngine;
using UnityEngine.UI;

public class WorldHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private Health _health;

    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        _health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(Health health) => _healthFill.fillAmount = health.CurrentHealth / health.MaxHealth;

    void Update() => transform.LookAt(_camera.transform);

    private void OnDisable() => _health.OnHealthChanged -= OnHealthChanged;
}
