using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMover), typeof(PlayerAttacker))]
public class PlayerAnimation : MonoBehaviour
{
    private const string IsIdle = "IsIdle";
    private const string Horizontal = "Horizontal";
    private const string Attack = "Attack";

    private Animator _animator;
    private PlayerMover _playerMove;
    private PlayerAttacker _playerAttacker;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMover>();
        _playerAttacker = GetComponent<PlayerAttacker>();

        _playerMove.Moved += OnMove;
        _playerMove.Idled += OnIdle;
        _playerAttacker.Attacked += OnAttack;
    }

    private void OnDisable()
    {
        _playerMove.Moved -= OnMove;
        _playerMove.Idled -= OnIdle;
        _playerAttacker.Attacked -= OnAttack;
    }

    private void OnIdle()
    {
        _animator.SetBool(IsIdle, true);
    }

    private void OnMove(float horizontal)
    {
        _animator.SetFloat(Horizontal, horizontal);
        _animator.SetBool(IsIdle, false);
    }

    private void OnAttack()
    {
        _animator.SetTrigger(Attack);
    }
}