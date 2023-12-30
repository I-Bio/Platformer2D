using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _health.Changed -= OnChanged;
    }

    private void OnChanged()
    {
        _bar.value = _health.CurrentHealth;
    }
}
