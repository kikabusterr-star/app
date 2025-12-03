using UnityEngine;

public class Health : MonoBehaviour
{
    [Tooltip("Maximum hit points")] public float maxHealth = 100f;

    private float current;
    public System.Action OnDeath;

    private void Awake()
    {
        current = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        current -= amount;
        if (current <= 0f)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
