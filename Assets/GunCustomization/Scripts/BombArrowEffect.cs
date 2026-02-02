using UnityEngine;

public class BombArrowEffect : MonoBehaviour
{
    public ParticleSystem bombSparksEffect;  // Reference to the bomb sparks particle system

    void Start()
    {
        // Ensure the sparks effect starts playing when the bomb arrow is equipped
        if (bombSparksEffect != null)
        {
            bombSparksEffect.Play();
        }
    }

    // Call this method if you want to stop the spark effect when the arrow is no longer equipped
    public void StopBombSparksEffect()
    {
        if (bombSparksEffect != null)
        {
            bombSparksEffect.Stop();
        }
    }
}
