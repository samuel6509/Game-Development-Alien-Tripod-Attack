using UnityEngine;

public class Damage : MonoBehaviour
{
    private GameObject bossHealth; // boss health script 
    private GameObject companion; // companion game object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHealth = GameObject.Find("tripod"); // find boss
        companion = GameObject.Find("AI Companion"); // find companion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // when collider on bullet collides with player or boss collider 
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            // reduce the boss health
            bossHealth.GetComponent<triPodHealth>().reduceHealth();

            Destroy(gameObject);
        }
        // if tag is companion
        if(other.gameObject.CompareTag("Companion"))
        {
            // reduce the companions health
            companion.GetComponent<HealthAI>().DecreaseHealth();

            Destroy(gameObject);
        }
    }
}
