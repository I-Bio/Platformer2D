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
    private bool _isFollowPlayer;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        SetTargetPoint();
        
        _enemyFollowingTrigger.PlayerEntered += SetTarget;
        _enemyFollowingTrigger.PlayerExited += OnPlayerExited;
    }

    private void OnDisable()
    {
        _enemyFollowingTrigger.PlayerEntered -= SetTarget;
        _enemyFollowingTrigger.PlayerExited -= OnPlayerExited;
    }

    private void Update()
    {
        Move();
    }
    
    public void SetNextPoint()
    {
        if (_isFollowPlayer == false)
        {
            _currentPoint++;

            if (_currentPoint >= _pathPoints.Length)
            {
                _currentPoint = 0;
            }

            SetTargetPoint();
        }
    }

    private void OnPlayerExited()
    {
        SetTargetPoint();
        StopFollow();
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
        _isFollowPlayer = true;
        _target = target;
    }

    private void StopFollow()
    {
        _isFollowPlayer = false;
    }
}
