using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Tooltip("How close the enemy must be to deal damage")] public float attackRange = 2.5f;
    [Tooltip("Damage dealt per successful attack")] public float attackDamage = 15f;
    [Tooltip("Seconds between attacks")] public float attackCooldown = 1.2f;

    private Transform target;
    private NavMeshAgent agent;
    private float nextAttack;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        var player = GameObject.FindWithTag("Player");
        target = player ? player.transform : null;
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        agent.SetDestination(target.position);

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= attackRange && Time.time >= nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            if (target.TryGetComponent<Health>(out var hp))
            {
                hp.TakeDamage(attackDamage);
            }
        }
    }
}
