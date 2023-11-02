using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMover))]
public class PlayerAnimation : MonoBehaviour
{
    private const string IsIdle = "IsIdle";
    private const string Horizontal = "Horizontal";
    
    private Animator _animator;
    private PlayerMover _playerMove;
    
    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMover>();
        
        _playerMove.Moved += OnMove;
        _playerMove.Idled += OnIdle;
    }

    private void OnDisable()
    {
        _playerMove.Moved -= OnMove;
        _playerMove.Idled -= OnIdle;
    }

    private void OnIdle()
    {
        _animator.SetBool(IsIdle, true);
    }
    
    private void OnMove(Vector2 moveDirection)
    {
        _animator.SetFloat(Horizontal, moveDirection.x);
        _animator.SetBool(IsIdle, false);
    }
}
