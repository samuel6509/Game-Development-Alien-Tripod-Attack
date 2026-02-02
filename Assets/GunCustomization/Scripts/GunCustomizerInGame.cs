using UnityEngine;
using UnityEngine.SceneManagement;

public class GunCustomizerInGame : MonoBehaviour
{
    public GameObject pistol; // Reference to the actual gun in the Alien Tripod scene
    public Transform barrelSlot;
    public Transform barrelGuardSlot;
    public Transform magTransform; // Reference to the magazine transform
    public GameObject[] barrels;
    public GameObject[] barrelGuards;
    private Magazine magazineScript;

    // Default and extended properties for the magazine
    private Vector3 defaultMagScale = new Vector3(1f, 1f, 1f);
    private Vector3 defaultMagPosition = new Vector3(0f, -0.11125f, -0.02999999f);
    private Vector3 extendedMagScale = new Vector3(1f, 2f, 1f);
    private Vector3 extendedMagPosition = new Vector3(0f, -0.26f, -0.03f);

    void Start()
    {
        // Get the Magazine component from the pistol in the Alien Tripod scene
        magazineScript = pistol.GetComponentInChildren<Magazine>();

        // Apply the saved customization
        ApplyCustomization();
        // my unlaod method is called when scene is unloaded
        SceneManager.sceneUnloaded += ApplyOnSceneUnload;
    }
    // method to apply the gun customization when the scene is unloaded
    private void ApplyOnSceneUnload(Scene scene)
    {
        if(scene.name == "GunCustomizationScene")
        {
            ApplyCustomization();
        }
    }
    void ApplyCustomization()
    {
        // Load the saved customization data from PlayerPrefs
        if (PlayerPrefs.HasKey("WeaponCustomization"))
        {
            string json = PlayerPrefs.GetString("WeaponCustomization");
            WeaponSaveData saveData = JsonUtility.FromJson<WeaponSaveData>(json);

            // Apply the barrel customization
            ApplyAttachment(barrelSlot, barrels, saveData.barrelIndex);

            // Apply the barrel guard customization
            ApplyAttachment(barrelGuardSlot, barrelGuards, saveData.barrelGuardIndex);

            // Apply the magazine customization
            ApplyMagazineCustomization(saveData.isExtendedMag);
        }
        else
        {
            Debug.LogWarning("No saved customization data found! Applying defaults.");
            ResetToDefaults();
        }
    }

    void ApplyMagazineCustomization(bool isExtended)
    {
        if (magTransform != null)
        {
            // Adjust the magazine's transform based on its state
            if (isExtended)
            {
                magTransform.localScale = extendedMagScale;
                magTransform.localPosition = extendedMagPosition;
                Debug.Log("Magazine set to extended size.");
            }
            else
            {
                magTransform.localScale = defaultMagScale;
                magTransform.localPosition = defaultMagPosition;
                Debug.Log("Magazine set to default size.");
            }
        }
        else
        {
            Debug.LogError("Magazine transform reference is missing!");
        }
    }

    // Function to apply a single attachment (barrel or barrel guard)
    void ApplyAttachment(Transform slot, GameObject[] attachments, int attachmentIndex)
    {
        // if there is no slot
        if (slot == null)
        {
            Debug.LogError("Attachment slot is null.");
            return;
        }
        // Remove any existing attachment in the slot
        for(int i = slot.childCount -1; i >= 0; i--)
        {
            Destroy(slot.GetChild(i).gameObject);
        }

        // Instantiate the new attachment if valid
        if (attachmentIndex >= 0 && attachmentIndex < attachments.Length)
        {
            GameObject newAttachment = Instantiate(attachments[attachmentIndex], slot);
            newAttachment.transform.localPosition = Vector3.zero;
            newAttachment.transform.localRotation = Quaternion.identity;
        }
    }

    // Reset to default states
    void ResetToDefaults()
    {
        // Reset barrel and barrel guard to their default states
        ApplyAttachment(barrelSlot, barrels, 0); // Assuming index 0 is the default
        ApplyAttachment(barrelGuardSlot, barrelGuards, 0);

        // Reset magazine to default state
        ApplyMagazineCustomization(false); // Default state is not extended
    }
}
