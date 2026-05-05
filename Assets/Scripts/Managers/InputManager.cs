using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }
    public event Action OnAttackButtonPressed;
    private Joystick joystick;



    private InputMaster inputMaster;
    private Vector2 mobileInputVector = new Vector2();
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        inputMaster = new InputMaster();
        inputMaster.Player.Attack.started += ctx => AttackButtonPressed();
        inputMaster.Enable();
        joystick = FindObjectOfType<Joystick>();
    }

    private void AttackButtonPressed()
    {
        if (!GameManager.Instance.IsPlaying())
        {
            inputMaster.Disable();
        }
        OnAttackButtonPressed?.Invoke();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        joystick = FindObjectOfType<Joystick>();
    }

    public void Update()
    {
        if (GameManager.Instance.IsPlaying())
        {
            HandleMobileInput();
        }
    }

    private void HandleMobileInput()
    {
        if (joystick != null)
        {
            mobileInputVector.x = joystick.Horizontal;
            mobileInputVector.y = joystick.Vertical;
        }
    }

    public Vector2 GetInputVectorNormalized()
    {
        if (!GameManager.Instance.IsPlaying())
        {
            return Vector2.zero;
        }
        Vector2 inputVector = inputMaster.Player.Move.ReadValue<Vector2>();

        if (joystick != null)
        {
            inputVector = mobileInputVector;
        }
        return inputVector.normalized;
    }
}
