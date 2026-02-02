using UnityEngine;

public class SmokeArrowEffect : MonoBehaviour
{
    public ParticleSystem smokeEffect;  // Reference to the smoke particle system

    void Start()
    {
        // Ensure the smoke effect starts playing when the arrow is equipped
        if (smokeEffect != null)
        {
            smokeEffect.Play();
        }
    }

    // Call this method if you want to stop the smoke effect when the arrow is no longer equipped
    public void StopSmokeEffect()
    {
        if (smokeEffect != null)
        {
            smokeEffect.Stop();
        }
    }
}
