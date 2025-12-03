using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;

    private float hp;

    public System.Action OnDeath;

    void Awake()
    {
        hp = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0f)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
