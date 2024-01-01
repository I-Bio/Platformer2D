using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMover), typeof(PlayerAttacker))]
public class PlayerAnimation : MonoBehaviour
{
    private const string IsIdle = "IsIdle";
    private const string Horizontal = "Horizontal";
    private const string Attack = "Attack";
    private const string LifeSteal = "LifeSteal";

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
        _playerAttacker.UsedLifeSteal += OnUseLifeSteal;
    }

    private void OnDisable()
    {
        _playerMove.Moved -= OnMove;
        _playerMove.Idled -= OnIdle;
        _playerAttacker.Attacked -= OnAttack;
        _playerAttacker.UsedLifeSteal -= OnUseLifeSteal;
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

    private void OnUseLifeSteal(float duration)
    {
        StartCoroutine(LifeStealDeactivating(duration));
    }

    private IEnumerator LifeStealDeactivating(float duration)
    {
        _animator.SetBool(LifeSteal, true);
        yield return new WaitForSeconds(duration);
        _animator.SetBool(LifeSteal, false);
    }
}