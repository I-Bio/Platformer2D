using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;
    
    private PlayerInput _playerInput;
    private bool _isAttacked;

    public event Action Attacked;

    private void OnEnable()
    {
        _playerInput = GetComponent<PlayerInput>();
        
        _playerInput.NeededAttack += StartAttack;
    }

    private void OnDisable()
    {
        _playerInput.NeededAttack -= StartAttack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _radius);
    }
    
    private void StartAttack()
    {
        if (_isAttacked == false)
        {
            _isAttacked = true;
            Attacked?.Invoke();
        }
    }
    
    private void EndAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _radius, _enemy);

        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }

        _isAttacked = false;
    }
}
