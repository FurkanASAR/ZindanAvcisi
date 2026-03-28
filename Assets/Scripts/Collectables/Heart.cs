using UnityEngine;

public class Heart : MonoBehaviour
{
    private float heal = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Heal(heal);
        }
    }
}
