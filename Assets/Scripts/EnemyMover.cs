using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform[] _pathPoints;
    [SerializeField] private float _speed;
    
    private Rigidbody2D _rigidbody2D;
    private int _currentPoint;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
    }

    private void Move()
    {
        Vector2 moveDirection = (_pathPoints[_currentPoint].position - transform.position).normalized;

        _rigidbody2D.velocity = moveDirection * _speed;
    }
}
