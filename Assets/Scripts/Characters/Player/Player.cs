using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IHasHealth, IHasInventory
{
    public static Player Instance { get; private set; }

    public event Action OnHealthChange;
    public HealthSystem CharacterHealth => playerHealth;
    public Faction Faction => faction;

    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float attackRate;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackPointRadius = 0.5f;
    [SerializeField] PlayerState playerState;

    private HealthSystem playerHealth;
    private float playerMaxHealth = 100f;
    private Vector2 inputVector = new Vector2();
    private float attackTimer;
    private bool isAttacking = false;
    private float damage = 10f;
    private Faction faction = Faction.Player;
    private enum PlayerState
    {
        Idle,
        Moving,
        Attacking
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerState = PlayerState.Idle;
        attackTimer = attackRate;
        playerHealth = new HealthSystem(gameObject,playerMaxHealth);        

        playerInventory = new InventorySystem();

        Debug.Log(playerHealth.GetHealth());
    }
    private void Start()
    {
        InputManager.Instance.OnAttackButtonPressed += HandleAttackEvent;
    }
    private void OnEnable()
    {

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
        isAttacking = true;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackPointRadius);
        foreach(Collider2D enemy in hitEnemies)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if(damageable != null && damageable.Faction == Faction.Enemy)
            {
                damageable.TakeDamage(damage);
            }
        }
        StartCoroutine(AttackDelay());
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackTimer);
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
    public void TakeDamage(float damage)
    {
        playerHealth.TakeDamage(damage);
        OnHealthChange?.Invoke();
        Debug.Log(playerHealth.GetHealth());
    }
    public void Heal(float heal)
    {
        playerHealth.Heal(heal);
        OnHealthChange?.Invoke();        
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

    #region Inventory
    private InventorySystem playerInventory;

    public InventorySystem Inventory => playerInventory;

    #endregion
}