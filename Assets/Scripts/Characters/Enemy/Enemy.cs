using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IHasHealth
{
    public HealthSystem CharacterHealth => enemyHealth;
    public event Action OnHealthChange;
    public Faction Faction => faction;

    private float damage = 10;
    private HealthSystem enemyHealth;
    private float maxHealth = 30f;
    private Faction faction = Faction.Enemy;

    private void Awake()
    {
        enemyHealth = new HealthSystem(gameObject, maxHealth);
    }
    public void Heal(float heal)
    {
        Debug.Log("Enemy Heal");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy takeDamage");
        enemyHealth.TakeDamage(damage);
        OnHealthChange?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && damageable.Faction == Faction.Player)
        {
            damageable.TakeDamage(damage);
        }
    }
}
