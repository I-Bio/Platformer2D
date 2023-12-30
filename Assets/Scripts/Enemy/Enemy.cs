using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health _health;

    private void OnEnable()
    {
        _health = GetComponent<Health>();

        _health.Died += OnDie;
    }
    
    private void OnDisable()
    {
        _health.Died -= OnDie;
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }
}