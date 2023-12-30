using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSmooth : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private Health _health;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _health.Changed += OnChange;
    }

    private void OnDisable()
    {
        _health.Changed -= OnChange;
    }

    private void OnChange()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeBar());
    }

    private IEnumerator ChangeBar()
    {
        while (_bar.value != _health.CurrentHealth)
        {
            _bar.value = Mathf.MoveTowards(_bar.value, _health.CurrentHealth, _speed);
            yield return null;
        }
    }
}
