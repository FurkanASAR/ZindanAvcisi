using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7;

    private InputMaster inputMaster;
    
    private void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }

    private void Update()
    {
        Vector2 inputVector = inputMaster.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0f);

        transform.position += moveDirection * Time.deltaTime * moveSpeed;
    }


}
