using UnityEngine;

public class BowShootingMechanic : MonoBehaviour
{
    public GameObject arrowPrefab;         // Prefab of the arrow to shoot
    public Transform bowTransform;         // Transform of the bow
    public float arrowSpeed = 20f;         // Speed of the arrow when shot

    void Update()
    {
        // Check if the player presses the 'F' key to shoot
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        if (arrowPrefab == null)
        {
            Debug.LogWarning("Arrow prefab is not assigned!");
            return;
        }

        // Instantiate the arrow at the bow's position and rotation
        GameObject arrowInstance = Instantiate(arrowPrefab, bowTransform.position, bowTransform.rotation);

        // Add Rigidbody to the arrow if it doesn't have one already
        Rigidbody arrowRigidbody = arrowInstance.GetComponent<Rigidbody>();
        if (arrowRigidbody == null)
        {
            arrowRigidbody = arrowInstance.AddComponent<Rigidbody>();
        }

        // Ensure the arrow is affected by physics
        arrowRigidbody.isKinematic = false;

        // Apply force to shoot the arrow forward
        Vector3 shootDirection = bowTransform.forward; // Use the bow's forward direction
        arrowRigidbody.linearVelocity = shootDirection * arrowSpeed;

        // Optional: Destroy the arrow after 5 seconds to avoid clutter
        Destroy(arrowInstance, 5f);
    }
}
