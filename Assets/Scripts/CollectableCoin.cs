using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerMover playerMover) == true)
        {
            Destroy(gameObject);
        }
    }
}
