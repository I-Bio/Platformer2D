using UnityEngine;

public class HealKit : MonoBehaviour
{
    [SerializeField] private float _healCount;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player) == true)
        {
            player.Heal(_healCount);
            Destroy(gameObject);
        }
    }
}
