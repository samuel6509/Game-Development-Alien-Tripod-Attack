// script to handle how much health the player
// also give info to companion FSM to become bullet sheild if player health gets too low
using UnityEngine;
using System.Collections;

// TODO:
// make it so you get hurt yourself

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 10f;
    public float playerMaxHealth = 10f;
    public float playerHealthRegen = 0.5f; // how much health is regenerated
    private Coroutine regen; // to monitor health regeneration

    public Canvas gameOverCanvas; // the canvas for GAME OVER screen
    public Canvas gameWinCanvas; // just in case you win at the same time still make it a loss as still dead
    public Canvas HUD; // canvas for the HUD
    public AudioClip LooseSound; // sound to be played upon loosing the game

    void Start()
    {
        Time.timeScale = 1f;
        Debug.Log("Timescale reset");
    }

    // method that takes away 1 health point from companion 
    public void DecreaseHealth()
    {
        playerHealth --;
        CheckHealth();
    }

    // method that chekcs if companion is dead
    public void CheckHealth()
    {
        if(playerHealth <= 0)
        {
            // pause the game in background
            Time.timeScale = 0;

            Debug.Log("GAME OVER");
            // change canvas on game loss
            gameOverCanvas.GetComponent<Canvas>().enabled = true;
            HUD.GetComponent<Canvas>().enabled = false;
            // just in case players wins at the same time as loosing, still make sure it's a loss
            gameWinCanvas.GetComponent<Canvas>().enabled = false;
            // play sound at players position for best sound 
            AudioSource.PlayClipAtPoint(LooseSound, transform.position);
            // take cursor out the scene & onto the canvas
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.Log("PLAYER has " + playerHealth + " health points remaining"); 
        }
    }

    // getter method for players current health
    public float CurrentHealth()
    {
        return playerHealth;
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
            playerHealth = Mathf.Min(playerHealth + playerHealthRegen, playerMaxHealth);
            Debug.Log("Player REGENERATED & now has " + playerHealth + " health points");
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

    // method to stop passively healing once player is at full health
    public void StopPassiveHealing()
    {
        if(playerHealth >= playerMaxHealth)
        {
            StopCoroutine(regen);
            regen = null;
            Debug.Log("REGENERATION STOPPED");
        }
    }

    // method for the restart button to make sure the game isnt still paused after retry
    public void Restart()
    {
        // put the game back into normal state
        Time.timeScale = 1f;
    }
}