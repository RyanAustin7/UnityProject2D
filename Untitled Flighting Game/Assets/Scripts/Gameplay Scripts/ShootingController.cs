using UnityEngine;
using System.Collections;

public class ShootingController : MonoBehaviour
{
    [Header("Bullet Stuff")]
    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletDestroyTime = 1.2f;
    public float fireRate = 0.32f;
    public KeyCode shootingKey = KeyCode.Space;

    [Header("Audio")]
    public AudioSource shootingAudioSource;
    public AudioClip shootingAudioClip;

    private float nextFireTime = 0f;
    [HideInInspector] public float originalFireRate; // Store original fire rate value

    private void Awake()
    {
        // Ensure that the AudioSource is assigned if not already
        if (shootingAudioSource == null)
        {
            shootingAudioSource = GetComponent<AudioSource>();
        }

        originalFireRate = fireRate; // Initialize original fire rate value
    }

    private void Update()
    {
        if (PauseMenu.isGamePaused)
            return; // Skip update if the game is paused

        HandleShooting();

        if (Time.timeScale == 0f)
        {
            return; // Skip shooting logic when the game is paused
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKey(shootingKey) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bulletClone = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        StartCoroutine(DestroyBulletAfterDelay(bulletClone));

        // Play shooting sound
        if (shootingAudioSource != null && shootingAudioClip != null)
        {
            shootingAudioSource.PlayOneShot(shootingAudioClip);
        }
    }

    private IEnumerator DestroyBulletAfterDelay(GameObject bulletClone)
    {
        yield return new WaitForSeconds(bulletDestroyTime);
        Destroy(bulletClone);  
    }

    public void SetFireRate(float newFireRate)
    {
        fireRate = newFireRate;
    }

    public void ResetShooting()
    {
        fireRate = originalFireRate;
        nextFireTime = 0f;
    }
}
