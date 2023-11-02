using UnityEngine;

public class PointEnterChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyMover enemyMover) == true)
        {
            enemyMover.SetNextPoint();
        }
    }
}
