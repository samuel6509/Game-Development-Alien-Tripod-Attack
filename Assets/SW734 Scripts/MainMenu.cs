using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // function called by start button to change scene to game when pressed
    public void loadGame()
    {
        SceneManager.LoadScene("Level01");
    }
}
