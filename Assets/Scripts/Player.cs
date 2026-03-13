using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7;

    private void OnEnable()
    {
        InputManager.Instance.OnAttackButtonPressed += Attack;
    }


    private void OnDisable()
    {
        InputManager.Instance.OnAttackButtonPressed -= Attack;
    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = new Vector2();
        inputVector = InputManager.Instance.GetInputVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0f);

        transform.position += moveDirection * Time.deltaTime * moveSpeed;
    }


    private void Attack()
    {
        Debug.Log("Player Attack!");
    }

}
