using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public string customizationSceneName = "GunCustomizationScene"; // Name of your Gun Customization scene
    public string mainSceneName = "Level01"; // Name of your main game scene
    public SmokeArrowEffect smokeEffectScript;

    void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Load the customization scene when "E" is pressed
            LoadCustomizationScene();
        }

        
    }

    // Method to load the customization scene
    public void LoadCustomizationScene()
    {
        // if the scene is not already loaded
        if(!SceneLoadedChecker(customizationSceneName))
        {
            Time.timeScale = 0; // Pause the game
            SceneManager.LoadScene(customizationSceneName, LoadSceneMode.Additive); // Load the scene additively
        }
    }

    // Method to return to the main game (to be called in the customization scene)
    public void ReturnToMainScene()
    {
        // Load the main scene first, then unload the customization scene
        //SceneManager.LoadScene(mainSceneName); // Load the AlienTripod scene
        SceneManager.UnloadSceneAsync(customizationSceneName); // Unload the customization scene
        Time.timeScale = 1; // Resume the game
        // take cursor out the scene & back into main game 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    // method that chekcs if the scene is already loaded if it is then it wont be able to be laoded again
    // if scene is already loaded return true if it isnt return false
    private bool SceneLoadedChecker(string sceneName)
    {
        // looping through to see if any of the already loaded scenes have the same name as the one trying to load 
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sceneLoad = SceneManager.GetSceneAt(i);
            if(sceneLoad.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}

