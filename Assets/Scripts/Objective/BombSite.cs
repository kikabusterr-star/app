using UnityEngine;

public class BombSite : MonoBehaviour
{
    public float plantRadius = 3f;

    public bool PlayerInSite(Vector3 pos)
    {
        return Vector3.Distance(transform.position, pos) <= plantRadius;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, plantRadius);
    }
}
