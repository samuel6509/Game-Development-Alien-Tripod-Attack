using UnityEngine;

public class Magazine : MonoBehaviour
{
    private bool extendedMag = false; // Whether the magazine is extended
    private int ammoCapacity = 50; // Default ammo capacity (for standard mag)
    private int extendedAmmoCapacity = 100; // Ammo capacity when extended

    private Vector3 defaultMagScale = new Vector3(1f, 1f, 1f); // Default scale
    private Vector3 extendedMagScale = new Vector3(1f, 2f, 1f); // Extended scale

    private Vector3 defaultMagPosition = new Vector3(0f, -0.11125f, -0.02999999f); // Default position
    private Vector3 extendedMagPosition = new Vector3(0f, -0.26f, -0.03f); // Extended position

    // Toggles the magazine between extended and default states
    public void ToggleMag()
    {
        if (extendedMag)
        {
            // Reset to default state
            transform.localScale = defaultMagScale;
            transform.localPosition = defaultMagPosition;
            ammoCapacity = 50; // Reset to default ammo capacity
            Debug.Log("Magazine reset to default size.");
        }
        else
        {
            // Extend the magazine
            transform.localScale = extendedMagScale;
            transform.localPosition = extendedMagPosition;
            ammoCapacity = extendedAmmoCapacity; // Set to extended ammo capacity
            Debug.Log("Magazine extended.");
        }

        extendedMag = !extendedMag; // Toggle the state
    }

    public int GetAmmoCapacity()
    {
        return ammoCapacity;
    }

    public void SetAmmoCapacity(int newCapacity)
    {
        ammoCapacity = newCapacity;
    }

    public bool IsExtended()
    {
        return extendedMag;
    }
}