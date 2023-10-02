using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorFilterController : MonoBehaviour
{
    private PostProcessVolume postProcessVolume;

    private ColorGrading colorGrading;
    private bool isBulletTimeActive = false;

    private void Start()
    {
        // Get the PostProcessVolume component attached to the camera
        postProcessVolume = GetComponent<PostProcessVolume>();
        
        // Make sure the PostProcessVolume component exists
        if (postProcessVolume != null)
        {
            // Get the ColorGrading effect from the volume
            postProcessVolume.profile.TryGetSettings(out colorGrading);
        }
    }

    public void ActivateBulletTimeEffect()
    {
        // Enable the color filter (you can adjust other settings here)
        Debug.Log("Activate filter");        
        colorGrading.enabled.value = true;
        isBulletTimeActive = true;
    }

    public void DeactivateBulletTimeEffect()
    {
        // Disable the color filter (you can adjust other settings here)
        colorGrading.enabled.value = false;
        isBulletTimeActive = false;
    }

    // You can call these methods when enabling/disabling bullet time
}
