using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject projectilePrefab;  // Reference to the projectile prefab
    public Transform firePoint;           // Point where the projectile is spawned
    public float fireForce = 10f;         // Force applied to the projectile

    private Transform player;             // Reference to the player's transform

    private void Start()
    {
        // Find the player by tag or other means (e.g., using a reference if already assigned)
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShootAtPlayer();
        }
    }

    private void ShootAtPlayer()
    {
        // Calculate the direction towards the player
        Vector3 direction = (player.position - firePoint.position).normalized;

        // Create a new projectile from the prefab
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Get the rigidbody of the projectile and apply force in the calculated direction
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.AddForce(direction * fireForce, ForceMode.Impulse);

        // Destroy the projectile after a certain time (adjust as needed)
        Destroy(newProjectile, 5f);
    }
}
