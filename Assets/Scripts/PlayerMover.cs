using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private LayerMask _groundMask; 
    [SerializeField] private float _bottomPointRadius;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;

    public event Action<Vector2> Moved;
    public event Action Idled;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    } 

    private void Update()
    {
        Jump();
        Move();
        CheckGround();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_bottomPoint.position, _bottomPointRadius);
    }

    private void Move()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal") * _speed, _rigidbody2D.velocity.y);
        bool isIdle = moveDirection.x == 0;

        if (isIdle == false)
        {
            Moved?.Invoke(moveDirection);
        }
        else
        {
            Idled?.Invoke();
        }

        _rigidbody2D.velocity = moveDirection;
    }
    
    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_bottomPoint.position, _bottomPointRadius, _groundMask);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && _isGrounded == true)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        }
    }
}
