using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private EnemyFollowingTrigger _enemyFollowingTrigger;
    [SerializeField] private Transform[] _pathPoints;
    [SerializeField] private float _speed;
    
    private Rigidbody2D _rigidbody2D;
    private Transform _target;
    private int _currentPoint;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        SetTargetPoint();
        
        _enemyFollowingTrigger.PlayerEntered += SetTarget;
        _enemyFollowingTrigger.PlayerExited += SetTargetPoint;
    }

    private void OnDisable()
    {
        _enemyFollowingTrigger.PlayerEntered -= SetTarget;
        _enemyFollowingTrigger.PlayerExited -= SetTargetPoint;
    }

    private void Update()
    {
        Move();
    }
    
    public void SetNextPoint()
    {
        _currentPoint++;

        if (_currentPoint >= _pathPoints.Length)
        {
            _currentPoint = 0;
        }
        
        SetTargetPoint();
    }

    private void Move()
    {
        Vector2 moveDirection = (_target.position - transform.position).normalized;
        moveDirection.y = 0f;

        _rigidbody2D.velocity = moveDirection * _speed;
    }

    private void SetTargetPoint()
    {
        _target = _pathPoints[_currentPoint];
    }

    private void SetTarget(Transform target)
    {
        _target = target;
    }
}
