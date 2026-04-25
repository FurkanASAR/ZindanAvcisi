using UnityEngine;

public class Heart : Item
{
    private IDamageable damageable;
    private float heal = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && damageable.Faction == Faction.Player)
        {
            this.damageable = damageable;
        }       
    }

    public override void Collect()
    {
        Debug.Log(damageable.CharacterHealth.GetHealth());
        Debug.Log("Heart Collect executed!");
        damageable.Heal(heal);
        Debug.Log(damageable.CharacterHealth.GetHealth());
        Destroy(gameObject);
    }
}
