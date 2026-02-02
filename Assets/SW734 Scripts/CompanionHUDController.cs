// script to link HUD for conotroller with relevent scripts 
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompanionHUDController : MonoBehaviour
{
    public Slider companionHealthSlider; // references the companion health bar 
    public GameObject Companion; // the companion gameobject
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        companionHealthSlider.value = Companion.GetComponent<HealthAI>().CurrentHealth() / 10f; // change this float to the same value of max healthpoints
    }
}
