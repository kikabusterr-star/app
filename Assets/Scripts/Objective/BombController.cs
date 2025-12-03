using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BombController : MonoBehaviour
{
    public float plantTime = 2.5f;
    public float fuseTime = 40f;
    public float explosionRadius = 10f;
    public float explosionDamage = 500f;
    public AudioClip plantSfx;
    public AudioClip beepSfx;
    public AudioClip explodeSfx;
    public BombSite targetSite;

    private bool planted;
    private float plantedAt;
    private AudioSource audioSrc;
    private bool planting;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (planted && Time.time - plantedAt >= fuseTime)
        {
            Explode();
        }
    }

    public System.Collections.IEnumerator TryPlant(Transform player)
    {
        if (planted || planting || targetSite == null)
        {
            yield break;
        }

        if (!targetSite.PlayerInSite(player.position))
        {
            yield break;
        }

        planting = true;
        float start = Time.time;

        while (Time.time - start < plantTime)
        {
            if (!Input.GetButton("Fire1"))
            {
                planting = false;
                yield break;
            }

            yield return null;
        }

        planted = true;
        plantedAt = Time.time;
        planting = false;

        if (plantSfx != null)
        {
            audioSrc.PlayOneShot(plantSfx);
        }

        if (beepSfx != null)
        {
            audioSrc.PlayOneShot(beepSfx);
        }
    }

    private void Explode()
    {
        if (explodeSfx != null)
        {
            audioSrc.PlayOneShot(explodeSfx);
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var col in hits)
        {
            if (col.TryGetComponent<Health>(out var hp))
            {
                hp.TakeDamage(explosionDamage);
            }
        }

        Destroy(gameObject, 0.2f);
    }
}
