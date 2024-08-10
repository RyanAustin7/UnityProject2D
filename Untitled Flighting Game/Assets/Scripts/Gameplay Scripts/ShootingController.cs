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

    private float nextFireTime = 0f;
    [HideInInspector] public float originalFireRate; // Store original fire rate value

    private void Awake()
    {
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

        // Play Wwise shooting sound based on the player's tag
        if (gameObject.CompareTag("Player1"))
        {
            AkSoundEngine.PostEvent("P1_Shoot", gameObject);
        }
        else if (gameObject.CompareTag("Player2"))
        {
            AkSoundEngine.PostEvent("P2_Shoot", gameObject);
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
