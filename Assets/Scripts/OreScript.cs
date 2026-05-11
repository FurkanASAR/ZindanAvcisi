using System;
using UnityEngine;

public class OreScript : MonoBehaviour, IDamageable, IHasHealth
{
    public GameObject coinPrefab;
    public HealthSystem CharacterHealth => oreHealth;
    public event Action OnHealthChange;
    public Faction Faction => faction;

    private HealthSystem oreHealth;
    private float maxHealth = 20f;
    private Faction faction = Faction.Ore;

    private void Awake()
    {
        oreHealth = new HealthSystem(gameObject, maxHealth);
        oreHealth.OnDeath += OreHealth_OnDeath;
    }

    private void OreHealth_OnDeath(object sender, EventArgs e)
    {
       Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }
    public void Heal(float heal)
    {
        Debug.Log("Enemy Heal");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy takeDamage");
        oreHealth.TakeDamage(damage);
        OnHealthChange?.Invoke();
    }
}