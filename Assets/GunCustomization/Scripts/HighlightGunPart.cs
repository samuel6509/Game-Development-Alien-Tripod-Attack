using UnityEngine;

public class HighlightGunPart : MonoBehaviour
{
    private Material originalMaterial;
    public Material highlightMaterial;

    private void OnMouseEnter()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
            renderer.material = highlightMaterial;
        } else {
            Debug.Log("Renderer is null");
        }
    }

    private void OnMouseExit()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && originalMaterial != null)
        {
            renderer.material = originalMaterial;
        }
    }
}
