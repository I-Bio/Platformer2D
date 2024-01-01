using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<float> NeededMove;
    public event Action NeededJump;
    public event Action NeededAttack;
    public event Action NeededLifeSteal;
    
    private void Update()
    {
        TransferHorizontal();
        CheckPressedKeyCode();
    }

    private void TransferHorizontal()
    {
        NeededMove?.Invoke(Input.GetAxis("Horizontal"));
    }

    private void CheckPressedKeyCode()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
            NeededJump?.Invoke();
        
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
            NeededAttack?.Invoke();
        
        if (Input.GetKeyDown(KeyCode.F) == true)
            NeededLifeSteal?.Invoke();
    }
}
