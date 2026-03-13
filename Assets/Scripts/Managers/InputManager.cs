using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }
    public event Action OnAttackButtonPressed;



    private InputMaster inputMaster;

    private void Awake()
    {
        Instance = this;
        inputMaster = new InputMaster();

        inputMaster.Player.Attack.performed += ctx => AttackButtonPressed();

        inputMaster.Enable();
    }

    private void AttackButtonPressed()
    {
        if (!GameManager.Instance.IsPlaying())
        {
            inputMaster.Disable();
        }
        OnAttackButtonPressed?.Invoke();
    }

    public Vector2 GetInputVectorNormalized()
    {
        if (!GameManager.Instance.IsPlaying())
        {
            inputMaster.Disable(); 
        }
        Vector2 inputVector = new Vector2();
        inputVector = inputMaster.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
