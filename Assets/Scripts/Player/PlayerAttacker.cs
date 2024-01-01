using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(LifeStealer))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;

    private LifeStealer _lifeStealer;
    private PlayerInput _playerInput;
    private bool _isAttacked;

    public event Action NeededForbidMoving;
    public event Action NeededAllowMoving;
    public event Action Attacked;
    public event Action<float> UsedLifeSteal;

    private void OnEnable()
    {
        _playerInput = GetComponent<PlayerInput>();
        _lifeStealer = GetComponent<LifeStealer>();
        _lifeStealer.SetEndAction(AllowAttack);
        
        _playerInput.NeededAttack += StartAttack;
        _playerInput.NeededLifeSteal += UseLifeSteal;
    }

    private void OnDisable()
    {
        _playerInput.NeededAttack -= StartAttack;
        _playerInput.NeededLifeSteal -= UseLifeSteal;
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
            ForbidAttack();
            Attacked?.Invoke();
        }
    }
    
    private void EndAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _radius, _enemy);

        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out Health health) == true)
            {
                health.TakeDamage(_damage);
            }
        }

        AllowAttack();
    }

    private void UseLifeSteal()
    {
        if (_isAttacked == false && _lifeStealer.CanSteal == true)
        {
            ForbidAttack();
            UsedLifeSteal?.Invoke(_lifeStealer.Duration);
            _lifeStealer.StartStealing();   
        }
    }

    private void AllowAttack()
    {
        _isAttacked = false;
        NeededAllowMoving?.Invoke();
    }

    private void ForbidAttack()
    {
        _isAttacked = true;
        NeededForbidMoving?.Invoke();
    }
}
