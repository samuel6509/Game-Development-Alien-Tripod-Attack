using UnityEngine;
using UnityEngine.UI;
using TMPro;

// TODO:
// add a health bar for companion above the companion
public class HUDController : MonoBehaviour
{
    public Slider bossHealthBar; // references the boss health bar 
    public TMP_Text ammoCount; // references the ammo count 

    public Slider playerHealthBar; // players health bar 

    public GameObject shooterObject, triPodObject, playerObject, gunShooterObject; // references for the objects with the scripts I want

    public TMP_Text gunAmmoCount; // pistols ammo count 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gets the current health of the boss & displays it on the slider
        bossHealthBar.value = triPodObject.GetComponent<triPodHealth>().displayHealth() / 20.0f; // change this float to the same value of bosses max healthpoints

        playerHealthBar.value = playerObject.GetComponent<PlayerHealth>().CurrentHealth() / 10.0f;

        // gets the amount of ammo left for Powercell Weapon
        ammoCount.text = "" + shooterObject.GetComponent<shooter>().CellCount();

        // gets the amount of ammo left for the pistol
        gunAmmoCount.text = "" + gunShooterObject.GetComponent<GunShooter>().BulletCount();
    }
}
