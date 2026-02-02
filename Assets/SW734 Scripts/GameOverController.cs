// script to be used by gameOverCanvas
// attach to the buttons so you get taken back to desired scene
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // method that restarts the game if the button it is assigned to is pressed
    public void restartLevel()
    {
        SceneManager.LoadScene("Level01"); 
    }

    // method for when main menu button is pressed take you back to main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
