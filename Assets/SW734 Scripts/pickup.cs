using UnityEngine;

public class pickup : MonoBehaviour
{
    public AudioClip pickupSound; // Set pickup sound in inspector 
    public float rotateSpeed = 175; //the speed the pickup will rotate

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0); //constantly rotating
    }

    // when comething collides with the pickup object
    void OnTriggerEnter(Collider other)
    {
        // if the player collides with a pickup object
        if(other.CompareTag("Player"))
        {
            Pickup(other.gameObject);
        }
    }

    // if a pickup sound has been assigned play it
    void Pickup(GameObject player)
    {
        if(pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        // if script is found then increase cell count
        shooter script = player.GetComponentInChildren<shooter>(); //gets the child object that has the shooter script
        if (script != null)
        {
            script.AddCell();
        }
        // if script is not found make it known
        else
        {
            Debug.Log("Script Not Found - no powercell added");
        }

        Destroy(gameObject);
    } 
}