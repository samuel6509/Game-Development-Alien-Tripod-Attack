using UnityEngine;

public class BowAndArrowCustomizationManager : MonoBehaviour
{
    public GameObject bowPrefab;           // The bow GameObject
    public GameObject standardArrowPrefab; // Standard arrow prefab
    public GameObject smokeArrowPrefab;    // Smoke arrow prefab
    public GameObject bombArrowPrefab;     // Bomb arrow prefab

    private GameObject currentArrowInstance;  // This will store the instance of the current arrow
    private string currentArrowType = "Standard"; // Track the current arrow type

    void Start()
    {
        // Load the saved arrow type, or default to the standard arrow
        string savedArrow = PlayerPrefs.GetString("SelectedArrow", "Standard");
        LoadArrowFromSave(savedArrow);
    }

    // Method to swap arrows
    public void EquipArrow(GameObject arrowPrefab, string arrowType)
    {
        // If an arrow is currently equipped, destroy it
        if (currentArrowInstance != null)
        {
            Destroy(currentArrowInstance);
        }

        // Instantiate the new arrow and set it as a child of the bow
        currentArrowInstance = Instantiate(arrowPrefab, bowPrefab.transform);

        // Set the arrow's position, rotation, and scale as per your specifications
        currentArrowInstance.transform.localPosition = new Vector3(0.279f, 0.036f, 0);  // Position
        currentArrowInstance.transform.localRotation = Quaternion.Euler(0, 0, 90);      // Rotation
        currentArrowInstance.transform.localScale = new Vector3(1, 1, 1);               // Scale

        // Update the current arrow type
        currentArrowType = arrowType;
    }

    // Equip the Standard Arrow
    public void EquipStandardArrow()
    {
        EquipArrow(standardArrowPrefab, "Standard");
    }

    // Equip the Smoke Arrow
    public void EquipSmokeArrow()
    {
        EquipArrow(smokeArrowPrefab, "Smoke");
    }

    // Equip the Bomb Arrow
    public void EquipBombArrow()
    {
        EquipArrow(bombArrowPrefab, "Bomb");
    }

    // Cancel Customization
    public void CancelCustomization()
    {
        // Destroy the currently equipped arrow
        if (currentArrowInstance != null)
        {
            Destroy(currentArrowInstance);
        }

        // Equip the standard arrow
        EquipStandardArrow();

        // Save the standard arrow as the selected arrow
        currentArrowType = "Standard";
        SaveArrowSelection();

        Debug.Log("Arrow reverted to Standard and saved!");
    }

    // Save the current arrow selection
    public void SaveArrowSelection()
    {
        // Save the current arrow type to PlayerPrefs
        PlayerPrefs.SetString("SelectedArrow", currentArrowType);
        PlayerPrefs.Save();

        Debug.Log("Arrow selection saved: " + currentArrowType);
    }

    // Load an arrow type based on the saved value
    private void LoadArrowFromSave(string arrowType)
    {
        switch (arrowType)
        {
            case "Standard":
                EquipArrow(standardArrowPrefab, "Standard");
                break;
            case "Smoke":
                EquipArrow(smokeArrowPrefab, "Smoke");
                break;
            case "Bomb":
                EquipArrow(bombArrowPrefab, "Bomb");
                break;
            default:
                EquipArrow(standardArrowPrefab, "Standard");
                break;
        }
    }
}
