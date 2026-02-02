using UnityEngine;

public class BowAndArrowCustomizerInGame : MonoBehaviour
{
    public GameObject bowPrefab;               // The bow GameObject in the game scene
    public GameObject standardArrowPrefab;     // Standard arrow prefab
    public GameObject smokeArrowPrefab;        // Smoke arrow prefab
    public GameObject bombArrowPrefab;         // Bomb arrow prefab
    private GameObject currentArrowInstance;   // Stores the current arrow instance

    void Start()
    {
        // Load the saved arrow selection from PlayerPrefs
        string savedArrow = PlayerPrefs.GetString("SelectedArrow", "Standard");
        LoadArrowFromSave(savedArrow);
    }

    // Load the arrow based on the saved type
    private void LoadArrowFromSave(string arrowType)
    {
        // Destroy any currently equipped arrow
        if (currentArrowInstance != null)
        {
            Destroy(currentArrowInstance);
        }

        // Equip the appropriate arrow based on saved type
        switch (arrowType)
        {
            case "Standard":
                EquipArrow(standardArrowPrefab);
                break;
            case "Smoke":
                EquipArrow(smokeArrowPrefab);
                break;
            case "Bomb":
                EquipArrow(bombArrowPrefab);
                break;
            default:
                EquipArrow(standardArrowPrefab);
                break;
        }
    }

    // Equip the arrow to the bow
    private void EquipArrow(GameObject arrowPrefab)
    {
        // Instantiate the new arrow and set it as a child of the bow
        currentArrowInstance = Instantiate(arrowPrefab, bowPrefab.transform);

        // Set the arrow's position, rotation, and scale as per your specifications
        currentArrowInstance.transform.localPosition = new Vector3(0.279f, 0.036f, 0);  // Position
        currentArrowInstance.transform.localRotation = Quaternion.Euler(0, 0, 90);      // Rotation
        currentArrowInstance.transform.localScale = new Vector3(1, 1, 1);               // Scale
    }

    // Returns the currently equipped arrow instance
    public GameObject GetCurrentArrowInstance()
    {
        return currentArrowInstance;
    }
}
