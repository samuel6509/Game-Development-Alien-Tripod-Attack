using UnityEngine;

public class CustomizationUI : MonoBehaviour
{
    public SceneTransitionManager sceneTransitionManager; // Reference to the SceneTransitionManager script

    void Start()    {
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Ensure the cursor is unlocked (not locked to the center)
    }
    
    // This function is called when the "Return to Main" button is pressed
    public void OnReturnToGameButtonPressed()
    {
        // Call the method to return to the main scene
        sceneTransitionManager.ReturnToMainScene();
    }
}
