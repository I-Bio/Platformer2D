using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _minHealth;
    
    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public event Action Changed;
    public event Action Died;
    
    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        Changed?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (_currentHealth - damage > _minHealth)
        {
            _currentHealth -= damage;
        }
        else
        {
            _currentHealth = _minHealth;
            Died?.Invoke();
        }
        
        Changed?.Invoke();
    }

    public void Heal(float healAmount)
    {
        if (_currentHealth + healAmount < _maxHealth)
        {
            _currentHealth += healAmount;
        }
        else
        {
            _currentHealth = _maxHealth;
        }
        
        Changed?.Invoke();
    }
}
