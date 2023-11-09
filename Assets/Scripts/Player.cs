using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_currentHealth - damage > 0)
        {
            _currentHealth -= damage;
        }
        else
        {
            _currentHealth = 0;
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void Heal(float healing)
    {
        if (_currentHealth + healing <= _maxHealth)
        {
            _currentHealth += healing;
        }
        else
        {
            _currentHealth = _maxHealth;
        }
    }
    
    
    
}
