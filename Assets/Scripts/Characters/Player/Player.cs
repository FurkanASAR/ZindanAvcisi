using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float attackRate;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackPointRadius = 0.5f;

    private Vector2 inputVector = new Vector2();
    private float attackTimer;
    private bool isAttacking = false;
    private enum PlayerState
    {
        Idle,
        Moving,
        Attacking
    }
    [SerializeField] PlayerState playerState;

    private void Awake()
    {
        playerState = PlayerState.Idle;
        attackTimer = attackRate;
    }
    private void OnEnable()
    {
        InputManager.Instance.OnAttackButtonPressed += HandleAttackEvent;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnAttackButtonPressed -= HandleAttackEvent;
    }
    private void Update()
    {
        //HandleInput gelen input'a gore state degisir
        HandleInput();
    }

    private void FixedUpdate()
    {
        //HandleState state'e gore eylem degisir
        HandleState();
    }
    private void Move()
    {
        inputVector = InputManager.Instance.GetInputVectorNormalized();

        if (inputVector == Vector2.zero)
        {
            playerState = PlayerState.Idle;
        }
        else
        {
            playerState = PlayerState.Moving;
        }

        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0f);
        transform.position += moveDirection * Time.deltaTime * moveSpeed;
    }
    private void HandleAttackEvent()
    {   if (isAttacking)
        {
            return;
        }
        else
        {          
            playerState = PlayerState.Attacking;            
        }
    }
    private void Attack()
    {
        Debug.Log("Player Attack!");
        isAttacking = true;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackPointRadius);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit enemy");
        }
        StartCoroutine(AttackDelay());
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackTimer);

        Debug.Log("Attack Timer Ended!");
        isAttacking = false;
        playerState = PlayerState.Idle;
    }
    private void HandleInput()
    {
        if(playerState == PlayerState.Attacking)
        {
            return;
        }
        inputVector = InputManager.Instance.GetInputVectorNormalized();
        if(inputVector != Vector2.zero)
        {
            playerState = PlayerState.Moving;
        }
        else
        {
            playerState = PlayerState.Idle;
        }
    }
    private void HandleState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                return;

            case PlayerState.Moving:
                Move();
                break;

            case PlayerState.Attacking:
                if (isAttacking)
                {
                    break;

                }
                else
                {
                    Attack();
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable icollectable = collision.GetComponent<ICollectable>();
        if (icollectable != null)
        {
            icollectable.Collect();
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(attackPoint.position, attackPointRadius);
    }
}
