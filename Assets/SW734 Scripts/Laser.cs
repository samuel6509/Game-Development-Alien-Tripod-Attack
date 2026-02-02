using UnityEngine;

public class Laser : MonoBehaviour
{
    public float lifeTime = 1f; // how long the bullet is on scene for
    public float speed = 10f; // the speed of the laser
    private Vector3 targetPos; // position of the targer for specific laser
    private GameObject playerHealth; // player health script
    private GameObject companionHealth; // companion health script

    // gets the target from bulletspawner script & set it as that bullets target
    public void Target(Vector3 position)
    {
        targetPos = position;
    }

    void Start()
    {
        playerHealth = GameObject.Find("FirstPerson-AIO"); // find player
        companionHealth = GameObject.Find("AI Companion"); // find companion
        // if laser missed things with collider despawn after X amount of seconds
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            // reduce players health
            playerHealth.GetComponent<PlayerHealth>().DecreaseHealth();

            Destroy(gameObject);
        }  

        if (other.gameObject.CompareTag("Companion"))
        { 
            // reduce companions health
            companionHealth.GetComponent<HealthAI>().DecreaseHealth();

            Destroy(gameObject);
        }  
    } 
}
