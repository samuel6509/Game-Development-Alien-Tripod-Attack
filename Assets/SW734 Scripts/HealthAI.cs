// script containing all health relating things for the companion 
// mostly health decreasing & passive health regen
using UnityEngine;
using System.Collections;

public class HealthAI : MonoBehaviour
{
    public float companionMaxHealth = 10f; // max health companion can have
    public float companionHealth = 3f; // health of the companion
    public float companionHealthRegen = 0.5f; // how much health is regenerated per second
    public AudioClip deathSound; // companions death sound
    private GameObject player; // to get players position 

    private Coroutine regen; // to monitor health regeneration 

    void Start()
    {
        // get player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // getter method for companions current health
    public float CurrentHealth()
    {
        return companionHealth;
    }

    // method that takes away 1 health point from companion 
    public void DecreaseHealth()
    {
        companionHealth --;
        CheckHealth();
    }

    // method that chekcs if companion is dead
    public void CheckHealth()
    {
        // if health is 0 or below
        if(companionHealth <= 0)
        {
            Destroy(gameObject);
            // play companions death sound at the players position so they can hear it
            AudioSource.PlayClipAtPoint(deathSound, player.transform.position);
            Debug.Log("Companion is DEAD, NOOOOOOO!!!!!!");
        }
        else
        {
            Debug.Log("Companion has " + companionHealth + " health points remaining");  
        }
    }

    // method for the logic of passive healing
    // loops indefinetely until stop method is called
    public IEnumerator PassiveHealing()
    {
        while (true)
        {
            // delay by 2 seconds
            yield return new WaitForSeconds(2f);

            // regen health, clamp to max health meaning dont go further
            companionHealth = Mathf.Min(companionHealth + companionHealthRegen, companionMaxHealth);
            Debug.Log("Companion REGENERATED & now has " + companionHealth + " health points");
            StopPassiveHealing();
        }
    }

    // method to start passive healing
    public void StartPassiveHealing()
    {
        // if not regenerating health
        if(regen == null)
        {
            regen = StartCoroutine(PassiveHealing());
        }
    }

    // method to stop passively healing once companion is at full health
    public void StopPassiveHealing()
    {
        if(companionHealth >= companionMaxHealth / 2)
        {
            StopCoroutine(regen);
            regen = null;
            Debug.Log("REGENERATION STOPPED");
        }
    }
}