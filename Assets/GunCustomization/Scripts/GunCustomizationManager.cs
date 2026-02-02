using UnityEngine;
using UnityEngine.UI;

public class GunCustomizationManager : MonoBehaviour
{
    [Header("Pistol and Slots")]
    public GameObject pistol; // Reference to the pistol
    public Transform barrelSlot; // Slot for the barrel attachment
    public Transform barrelGuardSlot; // Slot for the barrel guard attachment
    public Transform magTransform; // Transform of the magazine to change its size and position

    [Header("Attachments")]
    public GameObject[] barrels; // List of barrel attachments
    public GameObject[] barrelGuards; // List of barrel guard attachments

    private int currentBarrelIndex = 0;
    private int currentBarrelGuardIndex = 0;

    [Header("UI Buttons")]
    public Button nextBarrelButton;
    public Button nextBarrelGuardButton;
    public Button cancelButton; // Button for canceling customization
    public Button saveButton;  // Button for saving customization

    private Magazine magazineScript; // Reference to the Magazine script

    // Variables for saving and loading
    private bool extendedMag = false;  // Track extended magazine state

    void Start()
    {
        // Get the Magazine component from the pistol's child object (assuming it's attached to the magazine)
        magazineScript = pistol.GetComponentInChildren<Magazine>();

        if (magazineScript == null)
        {
            Debug.LogError("Magazine script not found!");
        }

        // Set up button listeners
        // cancelButton.onClick.AddListener(CancelCustomization);
        // saveButton.onClick.AddListener(SaveCustomization);

        // Optionally load the saved customization when the game starts
        LoadCustomization();
    }

    // Function to cycle through barrel attachments
    public void CycleBarrel()
    {
        currentBarrelIndex = (currentBarrelIndex + 1) % barrels.Length;
        UpdateAttachment(barrelSlot, barrels, currentBarrelIndex);
    }

    // Function to cycle through barrel guard attachments
    public void CycleBarrelGuard()
    {
        currentBarrelGuardIndex = (currentBarrelGuardIndex + 1) % barrelGuards.Length;
        UpdateAttachment(barrelGuardSlot, barrelGuards, currentBarrelGuardIndex);
    }

    // Function to change the magazine (toggle extend or reset)
    public void ChangeMagTransform()    {
        if (magazineScript != null)
        {
            magazineScript.ToggleMag(); // Toggle the magazine state
            extendedMag = magazineScript.IsExtended(); // Update the internal state
            SaveCustomization(); // Save the updated state
            Debug.Log("Magazine state saved: " + extendedMag);
        }
    }

    // Function to cancel customization and reset to defaults
    public void CancelCustomization()
    {
        // Reset to default scale, position, and rotation for the magazine
        if (magazineScript != null)
        {
            Debug.Log("ToggleMag function being called from the cancel button");
            if (extendedMag == true) {
                magazineScript.ToggleMag(); // Reset to default state
            } 
        }
        

        // Destroy all attachments in the slots
        RemoveAttachment(barrelSlot);
        RemoveAttachment(barrelGuardSlot);

        // Set the indexes to indicate no attachments (or reset to default values)
        currentBarrelIndex = 0;
        currentBarrelGuardIndex = 0;
        extendedMag = false; // Reset extended mag state

        // Save this new "empty" state
        SaveCustomization();
    }

    // Function to save the current customization
    public void SaveCustomization()
    {
        Debug.Log($"Saving barrel index: {currentBarrelIndex}, barrel guard index: {currentBarrelGuardIndex}, extended mag: {extendedMag}");
        // Debug.Log(currentBarrelGuardIndex);
        // Debug.Log(currentBarrelGuardIndex);
        // Debug.Log(extendedMag);
        
        // Create a WeaponSaveData object with the current customization data
        WeaponSaveData saveData = new WeaponSaveData(currentBarrelIndex, currentBarrelGuardIndex, extendedMag);

        // Serialize the data into a JSON string
        string json = JsonUtility.ToJson(saveData);

        // Save the JSON string to PlayerPrefs
        PlayerPrefs.SetString("WeaponCustomization", json);
        PlayerPrefs.Save();

        Debug.Log("Customization saved!");
    }

    // Function to load the saved customizatiodn
    public void LoadCustomization() {
        // Check if customization data exists in PlayerPrefs
        if (PlayerPrefs.HasKey("WeaponCustomization"))
        {
            // Load the saved data from PlayerPrefs
            string json = PlayerPrefs.GetString("WeaponCustomization");

            // Deserialize the JSON string back into a WeaponSaveData object
            WeaponSaveData saveData = JsonUtility.FromJson<WeaponSaveData>(json);

            // Apply the loaded data to the customization
            currentBarrelIndex = saveData.barrelIndex;
            currentBarrelGuardIndex = saveData.barrelGuardIndex;
            extendedMag = saveData.isExtendedMag;

            // If the saved data is all set to default (i.e., no attachments), then reset
            if (currentBarrelIndex == 0 && currentBarrelGuardIndex == 0 && !extendedMag)
            {
                // Remove attachments if they are in a "no customization" state
                RemoveAttachment(barrelSlot);
                RemoveAttachment(barrelGuardSlot);
            }
            else
            {
                // Otherwise, apply the customization
                UpdateAttachment(barrelSlot, barrels, currentBarrelIndex);
                UpdateAttachment(barrelGuardSlot, barrelGuards, currentBarrelGuardIndex);
                //ChangeMagTransform(); // This will set the correct magazine state (extended or not)
                if (extendedMag && magazineScript != null)  {
                    magazineScript.ToggleMag();
                }
            }

            Debug.Log("Customization loaded!");
        }
        else
        {
            // If no saved customization data exists, load the default state
            Debug.Log("No customization data found, applying default state.");
            // You can reset to default settings here if needed, for example:
            currentBarrelIndex = 0;
            currentBarrelGuardIndex = 0;
            extendedMag = false;

            // Optionally, reset attachments and magazine state
            RemoveAttachment(barrelSlot);
            RemoveAttachment(barrelGuardSlot);
            ChangeMagTransform();
        }
    }


    // Function to update the attachment of the gun
    void UpdateAttachment(Transform slot, GameObject[] attachments, int currentIndex)
    {
        // Remove any existing attachment
        foreach (Transform child in slot)
        {
            Destroy(child.gameObject);
        }

        // Check if the attachment is valid
        if (attachments[currentIndex] != null)
        {
            GameObject newAttachment = Instantiate(attachments[currentIndex], slot);
            newAttachment.transform.localPosition = Vector3.zero;
            newAttachment.transform.localRotation = Quaternion.identity;

            // Check the tag to apply specific functionality
            if (newAttachment.CompareTag("Barrel"))
            {
                Debug.Log("Barrel attachment equipped. Damage increased by 5.");
                // Apply damage modifier here
            }
            else if (newAttachment.CompareTag("Optic"))
            {
                Debug.Log("Optic attachment equipped. Accuracy improved.");
                // Apply accuracy modifier here
            }
        }
        else
        {
            Debug.LogWarning("Attachment at index " + currentIndex + " is null!");
        }
    }

    // Function to remove attachments from a slot
    void RemoveAttachment(Transform slot)
    {
        // Destroy any existing attachment in the slot
        foreach (Transform child in slot)
        {
            Destroy(child.gameObject);
        }
    }

    // Function to update the ammo UI
    public void UpdateAmmoUI()
    {
        if (magazineScript != null)
        {
            // Here, you can update the UI to reflect the new ammo capacity
            Debug.Log("Current ammo capacity: " + magazineScript.GetAmmoCapacity());
        }
    }
}