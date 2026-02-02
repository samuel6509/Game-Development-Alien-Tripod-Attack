// script to let the AI companion shoot
// will be called in the targeting script to allow companion to shoot 
using UnityEngine;

public class CompanionShooting : MonoBehaviour
{
    public GameObject bullet; // the prefab for bullet
    public Transform bulletSpawPoint; // gets the current position of bullet spawner which will be the gun 
    public float bulletVelocity; // the speed of the bullet
    public float fireRate; // how many rounds per minute the gun shoots 

    public AudioClip shootingSound;

    private float timeBetweenBullets; // used to check enough time has passed to shoot again
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // method for the logic on AI shooting
    public void shoot(Vector3 bossHitPoint)
    {
        // if enough time has passed to be able to shoot again
        if(Time.time >= timeBetweenBullets)
        {
            // shot sound 
            AudioSource.PlayClipAtPoint(shootingSound, transform.position);
            // clone the bullet gameObject 
            GameObject bulletClone = Instantiate(bullet, bulletSpawPoint.position, bulletSpawPoint.rotation);
            // direction to the boss
            Vector3 bossDirection = (bossHitPoint - bulletSpawPoint.position).normalized;
            // get bullets rigidbody 
            Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
            // makes sure that bullet in fact has rigidbody
            if(rb != null)
            {
                // add velocity 
                rb.linearVelocity = bossDirection * bulletVelocity;
            }
            // calculates the time between shots in seconds
            timeBetweenBullets = Time.time + 1f / fireRate;
            Debug.DrawRay(bulletSpawPoint.position, bulletSpawPoint.forward * 5f, Color.red, 2f);
        }
    }
}
