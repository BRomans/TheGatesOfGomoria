using UnityEngine;

public class AttackZone : MonoBehaviour
{

    [SerializeField]
    private CannonController cannonController;

    // This method is called when another Collider enters the trigger zone.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has a specific tag (e.g., "Player").
        if (other.CompareTag("Player"))
        {
            // Call the shooting action or method from another script.
            // For example, you might have a CannonController script attached
            // to the cannon that handles shooting.
            if (cannonController != null)
            {
                cannonController.ShootAtPlayer();
            }
        }
    }
}
