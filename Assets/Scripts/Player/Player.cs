using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health), typeof(PlayerMover), typeof(PlayerAttacker))]
public class Player : MonoBehaviour
{
    private const string MainScene = "SampleScene";
    
    private Health _health;
    private PlayerMover _playerMover;
    private PlayerAttacker _playerAttacker;
    
    private void OnEnable()
    {
        _health = GetComponent<Health>();
        _playerMover = GetComponent<PlayerMover>();
        _playerAttacker = GetComponent<PlayerAttacker>();

        _health.Died += OnDie;
        _playerAttacker.NeededAllowMoving += OnNeededAllowMoving;
        _playerAttacker.NeededForbidMoving += OnNeededForbidMoving;
    }

    private void OnDisable()
    {
        _health.Died -= OnDie;
        _playerAttacker.NeededAllowMoving -= OnNeededAllowMoving;
        _playerAttacker.NeededForbidMoving -= OnNeededForbidMoving;
    }

    private void OnDie()
    {
        SceneManager.LoadScene(MainScene);
    }

    private void OnNeededAllowMoving()
    {
        _playerMover.AllowMove();
    }

    private void OnNeededForbidMoving()
    {
        _playerMover.ForbidMove();
    }
}
