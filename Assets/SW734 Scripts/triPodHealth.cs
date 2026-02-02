using UnityEngine;

public class triPodHealth : MonoBehaviour
{
    public float maxHealth = 20f; // max health of tripod to be used in bossController script
    private float health = 20; //Health of tripod
    public GameObject smoke, flare; //refrencing smoke & flare object
    public Canvas gameWin; // canvas for winning the game
    public Canvas HUD; // canvas for the HUD

    public AudioClip winSound; // sound to be played upon winning the game
    private GameObject player; // to get players position 


    void Start()
    {
        // get player object
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void reduceHealth() //loosing 1 health point
    {
        health --;
        Debug.Log("Remaining Health =" + health);
        checkHealth();
    }

    public void checkHealth() //check if tripod is still alive
    {
        if (health <= 0){
            smoke.SetActive(true);
            flare.SetActive(true);
            Debug.Log("HUMANITY IS SAVED!!!");
            // change which canvas is enabled on game win & play sound
            gameWin.GetComponent<Canvas>().enabled = true;
            HUD.GetComponent<Canvas>().enabled = false;
            // play sound at players position for best sound 
            AudioSource.PlayClipAtPoint(winSound, player.transform.position);
            // take cursor out the scene & onto the canvas
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // function to be called by HUDController script, updates the health bar for the boss
    public float displayHealth()
    {
        return health;
    }

    // getter method for bosses current health
    public float CurrentHealth()
    {
        return health;
    }
}
