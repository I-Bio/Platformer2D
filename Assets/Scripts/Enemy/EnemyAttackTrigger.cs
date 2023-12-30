using System.Collections;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _damage;

    private bool _canAttack;

    private void Start()
    {
        _canAttack = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_canAttack == true && other.TryGetComponent(out Health health) == true)
        {
            _canAttack = false;
            health.TakeDamage(_damage);
            StartCoroutine(AttackRecharge());
        }
    }

    private IEnumerator AttackRecharge()
    {
        var waitTime = new WaitForSeconds(_attackDelay);
        yield return waitTime;
        _canAttack = true;
    }
}
