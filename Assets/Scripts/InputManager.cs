using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }

    private InputMaster inputMaster;

    private void Awake()
    {
        Instance = this;
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }

    public Vector2 GetInnputVectorNormalized()
    {
        Vector2 inputVector = new Vector2();
        inputVector = inputMaster.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
