using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponController : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public AudioClip shotSfx;
    public AudioClip reloadSfx;
    public float damage = 35f;
    public float range = 120f;
    public int magSize = 30;
    public float fireRate = 0.12f;
    public float reloadTime = 1.8f;

    private int ammoInMag;
    private float nextFire;
    private AudioSource audioSrc;
    private bool reloading;

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        ammoInMag = magSize;
    }

    void Update()
    {
        if (reloading)
        {
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFire && ammoInMag > 0)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

        if ((Input.GetKeyDown(KeyCode.R) || ammoInMag <= 0) && ammoInMag < magSize)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        ammoInMag--;
        muzzleFlash?.Play();
        if (shotSfx)
        {
            audioSrc.PlayOneShot(shotSfx);
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range))
        {
            if (hit.collider.TryGetComponent<Health>(out var hp))
            {
                hp.TakeDamage(damage);
            }
        }
    }

    System.Collections.IEnumerator Reload()
    {
        reloading = true;
        if (reloadSfx)
        {
            audioSrc.PlayOneShot(reloadSfx);
        }

        yield return new WaitForSeconds(reloadTime);
        ammoInMag = magSize;
        reloading = false;
    }
}
