// script for bullet to detect if it hit something
// if it misses then delete itself after defined amount of seconds
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f; // how long the bullet is on scene for
    private  static int playerShot = 0; // debugging to see if AI is hitting anything
    private static int bossShot = 0; // see if AI is hitting boss
    private GameObject playerHealth; // player health script
    private GameObject bossHealth; // boss health script 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = GameObject.Find("FirstPerson-AIO"); // find player
        bossHealth = GameObject.Find("tripod"); // find boss
        // if bullet missed things with collider despawn after X amount of seconds
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // when collider on bullet collides with player or boss collider 
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            playerShot ++;
            Debug.Log("Player has been shot: " + playerShot + " times");
            // reduce players health
            playerHealth.GetComponent<PlayerHealth>().DecreaseHealth();

            Destroy(gameObject);
        } 

        if(other.gameObject.CompareTag("Enemy"))
        {
            bossShot ++;
            Debug.Log("BOSS has been shot " + bossShot + " times");
            // reduce the boss health
            bossHealth.GetComponent<triPodHealth>().reduceHealth();

            Destroy(gameObject);
        }
    }
}
