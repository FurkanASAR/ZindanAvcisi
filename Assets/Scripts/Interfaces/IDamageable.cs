using System;
using UnityEngine;

public interface IDamageable
{
    public HealthSystem CharacterHealth{ get;}
    Faction Faction { get; }

    public event Action OnHealthChange;

    public void TakeDamage(float damage);
    public void Heal(float heal);
}
