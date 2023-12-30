using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    private const string MainScene = "SampleScene";
    
    private Health _health;
    
    private void OnEnable()
    {
        _health = GetComponent<Health>();

        _health.Died += OnDie;
    }

    private void OnDisable()
    {
        _health.Died -= OnDie;
    }

    private void OnDie()
    {
        SceneManager.LoadScene(MainScene);
    }
}
