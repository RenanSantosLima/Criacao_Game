using System;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;

    //movimentação
    public float Movement => playerControls.Gameplay.Movement.ReadValue<float>();

    //desativar os input
    public void DisablePlayerInput() => playerControls.Gameplay.Disable();

    //pulo
    public event Action OnJump;
    //ataque
    public event Action OnAttack;

    public InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Enable();

        //ação do pulo
        playerControls.Gameplay.Jump.performed += OnJumpPerformed;
        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }
}
