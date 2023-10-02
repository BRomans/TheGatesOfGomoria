using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviour
{
    private PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private bool isBulletTimeActive = false;

    private void Start()
    {
        // Get the PostProcessVolume component attached to the projectile
        postProcessVolume = GetComponent<PostProcessVolume>();
        
        // Make sure the PostProcessVolume component exists
        if (postProcessVolume != null)
        {
            // Get the Vignette effect from the volume
            postProcessVolume.profile.TryGetSettings(out vignette);
        }
    }

    public void ActivateBulletTimeEffect()
    {
        // Enable the vignette effect (you can adjust other settings here)
        vignette.enabled.value = true;
        isBulletTimeActive = true;
    }

    public void DeactivateBulletTimeEffect()
    {
        // Disable the vignette effect (you can adjust other settings here)
        vignette.enabled.value = false;
        isBulletTimeActive = false;
    }

    // You can call these methods when enabling/disabling bullet time
}
