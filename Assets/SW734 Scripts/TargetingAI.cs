// script to handle the targeting system for the companion AI
// to be used in the FSM to target enemy 
using UnityEngine;
using UnityEngine.AI;

public class TargetingAI : MonoBehaviour
{
    public Transform enemy; // the position of the enemy it is trying to hit
    public float detectionRange = 10f; // the range the companion can detect enemeies 
    private NavMeshAgent agent; // the navmesh used to find walkable path
    private CompanionShooting shootingScript; // script to actually shoot

    [SerializeField] private GameObject bulletSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        shootingScript = bulletSpawner.GetComponent<CompanionShooting>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void targetEnemy()
    {
        // if there is an enemy
        if(enemy != null)
        {
            // make sure the distance between enemy is valid 
            float distanceFromEnemy = Vector3.Distance(transform.position, enemy.position);
            // if an enemy is in the defined range
            if(distanceFromEnemy <= detectionRange)
            {
                // set distination to enemy
                agent.SetDestination(enemy.position);
                // call the shoot method 
                shootingScript.shoot(enemy.position);
            }
        }
    }
}
