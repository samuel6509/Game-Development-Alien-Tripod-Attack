// scene is dark (on purpose) but want to make it easier for player to see
// making a flashlight toggle so they can see 
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashLight; // reference to my light simulating a flashlight 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // makes sure the flashlight is off at beginning of the game
        flashLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if player presses Q on their keyboard
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // toggle the flashlight on or off
            flashLight.enabled = !flashLight.enabled;
        }
    }
}
