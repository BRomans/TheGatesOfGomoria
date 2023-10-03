using UnityEngine;
using System.Collections;

public class BulletTimeZone : MonoBehaviour
{

    [SerializeField]
    private CannonController cannonController;

    [SerializeField]
    private float BulletTime = 10f;
    private Rigidbody projectileRigidbody;
    private Vector3 originalVelocity;
    private bool isBulletTimeActive = false;

    // Reference to the VignetteController script on the projectile
    public VignetteController vignetteController;

    // This method is called when another Collider enters the trigger zone.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has a specific tag (e.g., "Player").
        if (other.CompareTag("Player"))
        {
            // Call the shooting action or method from another script.
            // For example, you might have a CannonController script attached
            // to the cannon that handles shooting.
            Debug.Log("Player detected!");
            if (cannonController != null)
            {
                cannonController.ShootAtPlayer();
            }
        }

        if (other.CompareTag("Projectile"))
        {
            

            // Access the Rigidbody of the projectile
            projectileRigidbody = other.GetComponent<Rigidbody>();

    
            // Enable the bullet time effect and vignette
            vignetteController = other.GetComponent<VignetteController>();
            vignetteController.ActivateBulletTimeEffect();

            if (projectileRigidbody != null && !isBulletTimeActive)
            {
                // Store the original velocity
                originalVelocity = projectileRigidbody.velocity;

                // Reduce the velocity to create the bullet time effect (divide by 10 for 10x reduction)
                projectileRigidbody.velocity /= 200f; // Reduces speed by 1000 times

                // Start a coroutine to reset the speed after 10 seconds
                cannonController.isBulletTimeActive = true;
                isBulletTimeActive = true;
                StartCoroutine(ResetBulletTime(BulletTime)); // 10 seconds delay
                
            }
        }
    }


    private IEnumerator ResetBulletTime(float delayInSeconds)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        // Disable the bullet time effect and color filter
        vignetteController.DeactivateBulletTimeEffect();

        // Check if the projectile Rigidbody is still valid
        if (projectileRigidbody != null)
        {
            // Restore the original velocity
            projectileRigidbody.velocity = originalVelocity;

            // Bullet time is no longer active
            isBulletTimeActive = false;
            cannonController.isBulletTimeActive = false;
        }
    }
}
