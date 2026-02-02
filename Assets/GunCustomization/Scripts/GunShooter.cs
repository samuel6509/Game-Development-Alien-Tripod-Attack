using UnityEngine;

public class GunShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Link to the bullet prefab
    public int noBullets = 30; // Number of bullets available
    public float bulletSpeed = 50f; // Bullet speed

    public Transform firePoint; // Where bullets spawn (usually the gun barrel)

    // Fire rate (cooldown between shots in seconds)
    public float fireRate = 0.1f;

    private float nextFireTime = 0f; // Time until the next shot can be fired
    private bool isShooting = false; // To avoid shooting multiple bullets in the same frame

    public AudioClip shootSound; // Sound to play when firing a bullet
    private AudioSource audioSource; // AudioSource component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Gun Shooter Initialized");

        // Ensure an AudioSource is attached or added to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Detect press of the 'F' key for firing
        if (Input.GetKeyDown(KeyCode.F) && noBullets > 0 && Time.time >= nextFireTime && !isShooting)
        {
            isShooting = true; // Prevent firing again in the same frame
            FireBullet();
            nextFireTime = Time.time + fireRate; // Set the next allowed fire time
        }

        // Reset shooting state after the cooldown period
        if (Time.time >= nextFireTime)
        {
            isShooting = false;
        }
    }

    // Fire a bullet
    void FireBullet()
    {
        if (noBullets > 0)
        {
            noBullets--;
            Debug.Log("Bullets Remaining: " + noBullets);

            // Instantiate the bullet at the fire point (e.g., gun barrel)
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Apply velocity and ensure no rotation
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.useGravity = true; // Enable gravity for realistic drop

                // Apply initial velocity
                bulletRb.linearVelocity = firePoint.forward * bulletSpeed;
                bulletRb.angularVelocity = firePoint.right * 10f; // Bullet spin for stabilization (adjust the value)
                bulletRb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze rotation in other axes

                // Apply drag for air resistance (optional)
                bulletRb.linearDamping = 0.1f;

                Debug.Log("Bullet Fired!");

                // Play the gunshot sound
                if (shootSound != null)
                {
                    audioSource.PlayOneShot(shootSound);
                }
            }
            else
            {
                Debug.LogWarning("Bullet prefab missing Rigidbody!");
            }
        }
        else
        {
            Debug.Log("No bullets left!");
        }
    }

    // Call this method to add bullets when picked up
    public void AddBullet()
    {
        noBullets++;
        Debug.Log("Bullet Count: " + noBullets);
    }

    // getter to be used for HUD 
    public int BulletCount()
    {
        return noBullets;
    }
}
