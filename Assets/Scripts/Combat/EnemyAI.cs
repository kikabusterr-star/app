using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public float attackRange = 2.5f;
    public float attackDamage = 15f;
    public float attackCooldown = 1.2f;

    private Transform player;
    private NavMeshAgent agent;
    private float nextAttack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (!player)
        {
            return;
        }

        agent.SetDestination(player.position);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange && Time.time >= nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            if (player.TryGetComponent<Health>(out var hp))
            {
                hp.TakeDamage(attackDamage);
            }
        }
    }
}
