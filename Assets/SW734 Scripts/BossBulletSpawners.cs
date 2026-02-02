using UnityEngine;

public class BossBulletSpawners : MonoBehaviour
{
    public GameObject laser; // the laser projectil the boss shoots out its eyes
    public Transform shootPoint; // where the lasers will be shot from
    public float fireRate = 1f; // how fast lasers shoot
    public Transform playerPosition; // position of player
    public Transform companionPosition; // position of the companion
    private float timeUntilShoot; // time until boss can shoot again
    public AudioClip laserSound; // the sound of the laser shooting out of the boss

    // Update is called once per frame
    void Update()
    {
        // if a second has passed then boss can shoot again
        if(Time.time >= timeUntilShoot)
        {
            shoot();
            timeUntilShoot = Time.time + 1f / fireRate;
        }  
    }

    // method that instantiates the laser & sets the specific target for the laser
    void shoot()
    {
        // pick at random wether to shoot at companion or player
        // keeps playing field fair & hopfully keeps the game from being too hard
        Transform target = Random.Range(0, 2) == 0 ? playerPosition : companionPosition;
        // if there is a target
        if(target != null)
        {
            GameObject laserClone = Instantiate(laser, shootPoint.position, shootPoint.rotation);
            // play laser shooting sound at their position
            AudioSource.PlayClipAtPoint(laserSound, transform.position);
            laserClone.GetComponent<Laser>().Target(target.position);
        }
    }
}
