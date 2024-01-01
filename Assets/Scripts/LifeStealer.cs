using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class LifeStealer : MonoBehaviour
{
    [SerializeField] private LayerMask _interact;
    [SerializeField] private float _stealAmount;
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;
    [SerializeField] private float _duration;
    [SerializeField] private float _coolDown;

    private Health _casterHealth;

    public event Action Started;
    public event Action NeededCoolDown;
    private event Action Ended;

    public bool CanSteal { get; private set; }
    public float CoolDown => _coolDown;
    public float Duration => _duration;

    private void Start()
    {
        _casterHealth = GetComponent<Health>();
        CanSteal = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void SetEndAction(Action ended)
    {
        Ended = ended;
    }

    public void StartStealing()
    {
        if (CanSteal == true)
        {
            CanSteal = false;
            Started?.Invoke();
            StartCoroutine(StealCycle());
        }
    }

    private IEnumerator StealCycle()
    {
        var waitTime = new WaitForSeconds(_delay);
        float elapsed = 0;

        while (elapsed < _duration)
        {
            Steal();
            yield return waitTime;
            elapsed += _delay;
        }

        NeededCoolDown?.Invoke();
        Ended?.Invoke();
        StartCoroutine(DropCoolDown());
    }

    private IEnumerator DropCoolDown()
    {
        yield return new WaitForSeconds(_coolDown);
        CanSteal = true;
    }

    private void Steal()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _radius, _interact);

        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out Health targetHealth) == true)
            {
                targetHealth.TakeDamage(_stealAmount);
                _casterHealth.Heal(_stealAmount);
            }
        }
    }
}