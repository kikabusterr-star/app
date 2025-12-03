using UnityEngine;

public class BombSite : MonoBehaviour
{
    [Tooltip("Radius around the site where planting is allowed")] public float plantRadius = 3f;

    public bool PlayerInSite(Vector3 position)
    {
        return Vector3.Distance(transform.position, position) <= plantRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, plantRadius);
    }
}
