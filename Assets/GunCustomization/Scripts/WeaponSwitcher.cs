using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public bool hasCustomizationUI = true; // Toggle for scenes with/without UI
    public CustomizationUIManager customizationUIManager; // UI Manager (optional)
    public GameObject gunModel; // Gun model
    public GameObject bowModel; // Bow model

    private enum WeaponType { Gun, Bow }
    private WeaponType currentWeapon;

    void Start()
    {
        // Load saved weapon state (default to Gun)
        currentWeapon = (WeaponType)PlayerPrefs.GetInt("CurrentWeapon", (int)WeaponType.Gun);

        // Equip the saved weapon
        EquipCurrentWeapon();
    }

    void Update()
    {
        // Switch weapons on key press
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(WeaponType.Gun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(WeaponType.Bow);
        }
    }

    void EquipWeapon(WeaponType weapon)
    {
        currentWeapon = weapon;

        if (weapon == WeaponType.Gun)
        {
            gunModel.SetActive(true);
            bowModel.SetActive(false);
            if (hasCustomizationUI)
            {
                customizationUIManager.ShowGunUI();
            }
        }
        else if (weapon == WeaponType.Bow)
        {
            gunModel.SetActive(false);
            bowModel.SetActive(true);
            if (hasCustomizationUI)
            {
                customizationUIManager.ShowBowUI();
            }
        }

        // Save weapon state
        PlayerPrefs.SetInt("CurrentWeapon", (int)currentWeapon);
        Debug.Log(currentWeapon + " Equipped");
    }

    void EquipCurrentWeapon()
    {
        if (currentWeapon == WeaponType.Gun)
        {
            EquipWeapon(WeaponType.Gun);
        }
        else if (currentWeapon == WeaponType.Bow)
        {
            EquipWeapon(WeaponType.Bow);
        }
    }
}
