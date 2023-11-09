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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.bounds.min.y > transform.position.y)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_canAttack == true && other.TryGetComponent(out Player player) == true)
        {
            _canAttack = false;
            player.TakeDamage(_damage);
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
