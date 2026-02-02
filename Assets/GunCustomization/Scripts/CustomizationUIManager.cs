using UnityEngine;

public class CustomizationUIManager : MonoBehaviour
{
    public GameObject gunCustomizationPanel; // Reference to the gun customization panel
    public GameObject bowCustomizationPanel; // Reference to the bow customization panel
    public GameObject gunCancelAndSavePanel; // Reference to the gun-specific Cancel/Save panel
    public GameObject bowCancelAndSavePanel; // Reference to the bow-specific Cancel/Save panel

    // Show the Gun UI and hide Bow UI
    public void ShowGunUI()
    {
        gunCustomizationPanel.SetActive(true); // Show gun UI
        bowCustomizationPanel.SetActive(false); // Hide bow UI

        gunCancelAndSavePanel.SetActive(true); // Show gun Cancel/Save panel
        bowCancelAndSavePanel.SetActive(false); // Hide bow Cancel/Save panel
    }

    // Show the Bow UI and hide Gun UI
    public void ShowBowUI()
    {
        gunCustomizationPanel.SetActive(false); // Hide gun UI
        bowCustomizationPanel.SetActive(true); // Show bow UI

        gunCancelAndSavePanel.SetActive(false); // Hide gun Cancel/Save panel
        bowCancelAndSavePanel.SetActive(true); // Show bow Cancel/Save panel
    }
}
