using System;
using UnityEngine;

public class EnemyFollowingTrigger : MonoBehaviour
{
    public event Action<Transform> PlayerEntered;
    public event Action PlayerExited;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player) == true)
        {
            PlayerEntered?.Invoke(player.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player) == true)
        {
            PlayerExited?.Invoke();
        }
    }
}
